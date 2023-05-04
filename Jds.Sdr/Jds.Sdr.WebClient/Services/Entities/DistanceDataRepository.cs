using Jds.Sdr.Shared.Data.EntityViews;
using Jds.Sdr.WebClient.Interfaces;

namespace Jds.Sdr.WebClient.Services.Entities
{
	public class DistanceDataRepository : DataRepositoryBase<DistanceView, int>
	{
		public DistanceDataRepository(HttpClient httpClient,
			IEntityControllerProvider entityControllerProvider)
			: base(httpClient, entityControllerProvider)
		{ }
	}
}
