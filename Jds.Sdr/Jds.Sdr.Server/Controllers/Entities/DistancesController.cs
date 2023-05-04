using AutoMapper;
using Jds.Sdr.Server.Attributes;
using Jds.Sdr.Server.Data;
using Jds.Sdr.Server.Data.Entities;
using Jds.Sdr.Shared.Data.EntityViews;
using Microsoft.EntityFrameworkCore;

namespace Jds.Sdr.Server.Controllers.Entities
{
	[EntityRoute(ApiController.Template, typeof(DistanceView), nameof(DistancesController))]
	public class DistancesController : EntityControllerBase<Distance, DistanceView>
	{
		public DistancesController(ApplicationDbContext dbContext, IMapper mapper)
			: base(dbContext, mapper) { }

		protected override IQueryable<Distance> GetAllAsQueryable()
		{
			return DbContext.Distances
				.Include(x => x.Branch)
				.Include(x => x.Employee)
				.AsNoTracking()
				.AsQueryable();
		}

		protected override Distance ClearRelatedEntities(Distance entity)
		{
			if (entity is not null)
			{
				entity.Branch = null;
				entity.Employee = null;
			}
			return entity;
		}
	}
}
