using AutoMapper;
using Jds.Sdr.Server.Attributes;
using Jds.Sdr.Server.Data;
using Jds.Sdr.Server.Data.Entities;
using Jds.Sdr.Shared.Data.EntityViews;
using Microsoft.EntityFrameworkCore;

namespace Jds.Sdr.Server.Controllers.Entities
{
	[EntityRoute(ApiController.Template, typeof(EmployeeView), nameof(EmployeesController))]
	public class EmployeesController : EntityControllerBase<Employee, EmployeeView>
	{
		public EmployeesController(ApplicationDbContext dbContext, IMapper mapper)
			: base(dbContext, mapper) { }

		protected override IQueryable<Employee> GetAllAsQueryable()
		{
			return DbContext.Employees
				.OrderBy(x => x.FirstName)
				.ThenBy(x => x.LastName)
				.AsNoTracking()
				.AsQueryable();
		}
	}
}
