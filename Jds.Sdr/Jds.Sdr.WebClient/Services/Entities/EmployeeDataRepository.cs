using Jds.Sdr.Shared.Data.EntityViews;
using Jds.Sdr.WebClient.Interfaces;

namespace Jds.Sdr.WebClient.Services.Entities
{
	public class EmployeeDataRepository : DataRepositoryBase<EmployeeView, int>
	{
		public EmployeeDataRepository(HttpClient httpClient,
			IEntityControllerProvider entityControllerProvider)
			: base(httpClient, entityControllerProvider)
		{ }
	}
}
