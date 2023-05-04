using Jds.Sdr.Shared.Data.EntityViews;
using Jds.Sdr.WebClient.Interfaces;

namespace Jds.Sdr.WebClient.Services.Entities
{
	public class RoleDataRepository : DataRepositoryBase<RoleView, int>
	{
		public RoleDataRepository(HttpClient httpClient,
			IEntityControllerProvider entityControllerProvider)
			: base(httpClient, entityControllerProvider)
		{ }
	}
}
