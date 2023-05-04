using AutoMapper;
using Jds.Sdr.Server.Attributes;
using Jds.Sdr.Server.Data;
using Jds.Sdr.Server.Data.Entities;
using Jds.Sdr.Shared.Data.EntityViews;
using Microsoft.EntityFrameworkCore;

namespace Jds.Sdr.Server.Controllers.Entities
{
	[EntityRoute(ApiController.Template, typeof(BranchView), nameof(BranchesController))]
	public class BranchesController : EntityControllerBase<Branch, BranchView>
	{
		public BranchesController(ApplicationDbContext dbContext, IMapper mapper)
			: base(dbContext, mapper) { }

		protected override IQueryable<Branch> GetAllAsQueryable()
		{
			return DbContext.Branches
				.OrderBy(x => x.Name)
				.AsNoTracking()
				.AsQueryable();
		}
	}
}
