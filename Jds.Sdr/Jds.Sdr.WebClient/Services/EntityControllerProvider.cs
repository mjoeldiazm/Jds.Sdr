using Jds.Sdr.Shared.Services;
using Jds.Sdr.WebClient.Interfaces;
using System.Text.Json;

namespace Jds.Sdr.WebClient.Services
{
	internal class EntityControllerProvider : IEntityControllerProvider
	{
		private const string EntityEndPointsRoute = "api/EntityEndPoints";

		private readonly HttpClient httpClient;
		private List<EntityControllerInformation> entityControllers;
		private readonly JsonSerializerOptions JsonSerializerOptions =
			new() { PropertyNameCaseInsensitive = true };

		public EntityControllerProvider(HttpClient httpClient)
		{
			this.httpClient = httpClient;
		}

		private async Task LoadControllers()
		{
			HttpResponseMessage httpResponse = await httpClient.GetAsync(EntityEndPointsRoute);
			if (httpResponse.IsSuccessStatusCode)
			{
				string responseString = await httpResponse.Content.ReadAsStringAsync();
				entityControllers = JsonSerializer
					.Deserialize<List<EntityControllerInformation>>(responseString, JsonSerializerOptions);
			}
		}

		public async Task<string> GetControllerRouteFor<TEntity>()
		{
			string result = string.Empty;
			if (entityControllers is null) await LoadControllers();
			EntityControllerInformation entityControllerInformation = entityControllers
				.FirstOrDefault(x => x.EntityName.Equals(typeof(TEntity).Name));
			if (entityControllerInformation is null)
			{
				return string.Empty;
			}
			return entityControllerInformation.EndPointRoute;
		}
	}
}
