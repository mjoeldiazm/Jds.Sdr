using Jds.Sdr.Shared.Data.EntityViews;
using Jds.Sdr.WebClient.Interfaces;

namespace Jds.Sdr.WebClient.Services.Entities
{
	public class TripDistanceDataRepository : DataRepositoryBase<TripDistanceView, int>
	{
		public TripDistanceDataRepository(HttpClient httpClient,
			IEntityControllerProvider entityControllerProvider)
			: base(httpClient, entityControllerProvider)
		{ }
	}
}
