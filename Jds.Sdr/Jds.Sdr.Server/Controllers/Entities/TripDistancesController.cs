using AutoMapper;
using Jds.Sdr.Server.Attributes;
using Jds.Sdr.Server.Data;
using Jds.Sdr.Server.Data.Entities;
using Jds.Sdr.Shared.Data.EntityViews;
using Microsoft.EntityFrameworkCore;

namespace Jds.Sdr.Server.Controllers.Entities
{
	[EntityRoute(ApiController.Template, typeof(TripDistanceView), nameof(TripDistancesController))]
	public class TripDistancesController : EntityControllerBase<TripDistance, TripDistanceView>
	{
		public TripDistancesController(ApplicationDbContext dbContext, IMapper mapper)
			: base(dbContext, mapper) { }

		protected override IQueryable<TripDistance> GetAllAsQueryable()
		{
			return DbContext.TripDistances
				.Include(x => x.Distance)
				.ThenInclude(x => x.Employee)
				.Include(x => x.Trip)
				.ThenInclude(x => x.Branch)
				.AsNoTracking()
				.AsQueryable();
		}

		protected override TripDistance ClearRelatedEntities(TripDistance entity)
		{
			if (entity is not null)
			{
				entity.Distance = null;
				entity.Trip = null;
			}
			return entity;
		}

		public override async Task<EntityOperationResult<TripDistance>> BeforePost(TripDistance objectToPost)
		{
			Distance distance = await DbContext.Distances
				.Where(x => x.Id == objectToPost.DistanceId)
				.FirstOrDefaultAsync();
			if (distance == null)
				return new EntityOperationResult<TripDistance>(objectToPost, false,
					"Distancia de empleado no encontrada.");
			bool containsEmployeeInSameDate = await DbContext.TripDistances
				.AnyAsync(x => x.Trip.Date == objectToPost.Trip.Date
					&& x.Distance.EmployeeId == distance.EmployeeId);
			if (containsEmployeeInSameDate)
				return new EntityOperationResult<TripDistance>(objectToPost, false,
					"Ya se ha registrado un viaje para este mismo colaborador en este día.");
			IEnumerable<TripDistance> tripDistances = await DbContext.TripDistances
				.Include(x => x.Distance)
				.Where(x => x.TripId == objectToPost.TripId)
				.ToListAsync();
			double totalTripDistance = tripDistances.Any() ?
				tripDistances.Sum(x => x.Distance.DistanceInKilometers) + distance.DistanceInKilometers : 0;
			if (totalTripDistance > 100D)
				return new EntityOperationResult<TripDistance>(objectToPost, false,
					"Al agregar este colaborador se supera la distancia máxima de 100 km para este viaje.");
			return new EntityOperationResult<TripDistance>(objectToPost, true);
		}
	}
}
