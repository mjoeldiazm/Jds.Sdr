using AutoMapper;
using Jds.Sdr.Server.Attributes;
using Jds.Sdr.Server.Data;
using Jds.Sdr.Server.Data.Entities;
using Jds.Sdr.Shared.Data.EntityViews;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Jds.Sdr.Server.Controllers.Entities
{
	[EntityRoute(ApiController.Template, typeof(TripView), nameof(TripsController))]
	public class TripsController : EntityControllerBase<Trip, TripView>
	{
		private readonly UserManager<UserAccount> userManager;

		public TripsController(ApplicationDbContext dbContext, IMapper mapper, UserManager<UserAccount> userManager)
			: base(dbContext, mapper)
		{
			this.userManager = userManager;
		}

		protected override IQueryable<Trip> GetAllAsQueryable()
		{
			return DbContext.Trips
				.Include(x => x.Branch)
				.Include(x => x.Creator)
				.Include(x => x.Carrier)
				.Include(x => x.TripDistances)
				.ThenInclude(x => x.Distance)
				.OrderBy(x => x.Date)
				.AsNoTracking()
				.AsQueryable();
		}

		protected override Trip ClearRelatedEntities(Trip entity)
		{
			if (entity is not null)
			{
				entity.Branch = null;
				entity.Creator = null;
				entity.Carrier = null;
			}
			return entity;
		}

		public async override Task<EntityOperationResult<Trip>> BeforePost(Trip objectToPost)
		{
			UserAccount currentUserAccount = await userManager
				.FindByEmailAsync(HttpContext.User.Identity.Name);
			if (currentUserAccount is null)
				return new EntityOperationResult<Trip>(objectToPost, false);
			else
				objectToPost.CreatorId = currentUserAccount.Id;
			return new EntityOperationResult<Trip>(objectToPost, true);
		}
	}
}
