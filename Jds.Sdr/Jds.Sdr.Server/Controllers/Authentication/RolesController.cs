using AutoMapper;
using Jds.Sdr.Server.Attributes;
using Jds.Sdr.Server.Data;
using Jds.Sdr.Server.Data.Entities;
using Jds.Sdr.Shared.Data.EntityViews;
using Microsoft.EntityFrameworkCore;

namespace Jds.Sdr.Server.Controllers.Authentication
{
	[EntityRoute(ApiController.Template, typeof(RoleView), nameof(RolesController))]
	public class RolesController : EntityControllerBase<Role, RoleView>
	{
		public RolesController(ApplicationDbContext dbContext, IMapper mapper)
			: base(dbContext, mapper) { }

		protected override IQueryable<Role> GetAllAsQueryable()
		{
			return DbContext.Roles
				.OrderBy(x => x.Name)
				.AsNoTracking()
				.AsQueryable();
		}
	}
}
