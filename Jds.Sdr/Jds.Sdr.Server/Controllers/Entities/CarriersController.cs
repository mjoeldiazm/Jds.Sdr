using AutoMapper;
using Jds.Sdr.Server.Attributes;
using Jds.Sdr.Server.Data;
using Jds.Sdr.Server.Data.Entities;
using Jds.Sdr.Shared.Data.EntityViews;
using Microsoft.EntityFrameworkCore;

namespace Jds.Sdr.Server.Controllers.Entities
{
	[EntityRoute(ApiController.Template, typeof(CarrierView), nameof(CarriersController))]
	public class CarriersController : EntityControllerBase<Carrier, CarrierView>
	{
		public CarriersController(ApplicationDbContext dbContext, IMapper mapper)
			: base(dbContext, mapper) { }

		protected override IQueryable<Carrier> GetAllAsQueryable()
		{
			return DbContext.Carriers
				.OrderBy(x => x.FirstName)
				.ThenBy(x => x.LastName)
				.AsNoTracking()
				.AsQueryable();
		}
	}
}
