using Jds.Sdr.Shared.Data.EntityViews;
using Jds.Sdr.Shared.Interfaces;
using Jds.Sdr.Shared.Security;
using Jds.Sdr.Shared.Services;
using Jds.Sdr.WebClient.Interfaces;
using System.Net.Mime;
using System.Text;
using System.Text.Json;

namespace Jds.Sdr.WebClient.Services.Entities
{
	public class UserAccountDataRepository : DataRepositoryBase<UserAccountView, int>, IUserAccountLogin
	{
		public UserAccountDataRepository(HttpClient httpClient, IEntityControllerProvider entityControllerProvider) : base(httpClient, entityControllerProvider) { }

		public async Task<DataServiceResponse<UserAccountLoginInformation>> Login(UserAccountCredential userAccountCredential)
		{
			await LoadEntityControllerRoutes();
			string serializedObjectToPost = JsonSerializer.Serialize(userAccountCredential);
			StringContent contentToPost = new(
				serializedObjectToPost, Encoding.UTF8, MediaTypeNames.Application.Json);
			HttpResponseMessage httpResponse = await HttpClient
				.PostAsync($"{EntityControllerRoute}/Login", contentToPost);
			if (!httpResponse.IsSuccessStatusCode)
				return new DataServiceResponse<UserAccountLoginInformation>(true, httpResponse.StatusCode.ToString());
			string responseString = await httpResponse.Content.ReadAsStringAsync();
			DataServiceResponse<UserAccountLoginInformation> serviceResponse = JsonSerializer
					.Deserialize<DataServiceResponse<UserAccountLoginInformation>>(responseString, JsonSerializerOptions);
			return serviceResponse;
		}

		public async Task<DataServiceResponse<UserAccountTokenView>> RenewToken()
		{
			await LoadEntityControllerRoutes();
			HttpResponseMessage httpResponse = await HttpClient.GetAsync($"{EntityControllerRoute}/RenewToken");
			if (!httpResponse.IsSuccessStatusCode)
				return new DataServiceResponse<UserAccountTokenView>(true, httpResponse.StatusCode.ToString());
			string responseString = await httpResponse.Content.ReadAsStringAsync();
			DataServiceResponse<UserAccountTokenView> serviceResponse = JsonSerializer
					.Deserialize<DataServiceResponse<UserAccountTokenView>>(responseString, JsonSerializerOptions);
			return serviceResponse;
		}
	}
}
