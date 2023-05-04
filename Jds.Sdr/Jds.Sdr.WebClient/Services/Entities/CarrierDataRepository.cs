using Jds.Sdr.Shared.Data.EntityViews;
using Jds.Sdr.WebClient.Interfaces;

namespace Jds.Sdr.WebClient.Services.Entities
{
	public class CarrierDataRepository : DataRepositoryBase<CarrierView, int>
	{
		public CarrierDataRepository(HttpClient httpClient,
			IEntityControllerProvider entityControllerProvider)
			: base(httpClient, entityControllerProvider)
		{ }
	}
}
