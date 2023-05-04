using Jds.Sdr.Shared.Interfaces;
using Jds.Sdr.Shared.Services;
using Jds.Sdr.WebClient.Interfaces;
using System.Net.Mime;
using System.Text;
using System.Text.Json;

namespace Jds.Sdr.WebClient.Services
{
	public abstract class DataRepositoryBase<TEntity, TId> : IDataRepository<TEntity, TId>
		where TEntity : IDataEntity<TId>
	{
		private readonly HttpClient httpClient;
		private readonly IEntityControllerProvider entityControllerProvider;
		private readonly JsonSerializerOptions jsonSerializerOptions = new()
		{
			PropertyNameCaseInsensitive = true
		};

		protected string EntityControllerRoute { get; set; }

		protected HttpClient HttpClient
		{
			get => httpClient;
		}

		protected JsonSerializerOptions JsonSerializerOptions
		{
			get => jsonSerializerOptions;
		}

		public DataRepositoryBase(HttpClient httpClient, IEntityControllerProvider entityControllerProvider)
		{
			this.httpClient = httpClient;
			this.entityControllerProvider = entityControllerProvider;
		}

		public virtual async Task<DataServiceResponse<IEnumerable<TEntity>>> GetAllAsync()
		{
			await LoadEntityControllerRoutes();
			HttpResponseMessage httpResponse = await HttpClient.GetAsync(EntityControllerRoute);
			if (!httpResponse.IsSuccessStatusCode)
				return new DataServiceResponse<IEnumerable<TEntity>>(true, httpResponse.StatusCode.ToString());
			string responseString = await httpResponse.Content.ReadAsStringAsync();
			DataServiceResponse<IEnumerable<TEntity>> serviceResponse = JsonSerializer
					.Deserialize<DataServiceResponse<IEnumerable<TEntity>>>(responseString, JsonSerializerOptions);
			return serviceResponse;
		}

		public virtual async Task<DataServiceResponse<TId>> PostAsync(TEntity objectToPost)
		{
			await LoadEntityControllerRoutes();
			string serializedObjectToPost = JsonSerializer.Serialize(objectToPost);
			StringContent contentToPost = new(serializedObjectToPost, Encoding.UTF8, MediaTypeNames.Application.Json);
			HttpResponseMessage httpResponse = await HttpClient
				.PostAsync(EntityControllerRoute, contentToPost);
			if (!httpResponse.IsSuccessStatusCode)
				return new DataServiceResponse<TId>(true, httpResponse.StatusCode.ToString());
			string responseString = await httpResponse.Content.ReadAsStringAsync();
			DataServiceResponse<TId> serviceResponse = JsonSerializer
					.Deserialize<DataServiceResponse<TId>>(responseString, JsonSerializerOptions);
			return serviceResponse;
		}

		public virtual async Task<DataServiceResponse<TId>> PutAsync(TEntity objectToPut)
		{
			await LoadEntityControllerRoutes();
			string serializedObjectToPut = JsonSerializer.Serialize(objectToPut);
			StringContent contentToPut = new(serializedObjectToPut, Encoding.UTF8, MediaTypeNames.Application.Json);
			HttpResponseMessage httpResponse = await HttpClient
				.PutAsync(EntityControllerRoute, contentToPut);
			if (!httpResponse.IsSuccessStatusCode)
				return new DataServiceResponse<TId>(true, httpResponse.StatusCode.ToString());
			string responseString = await httpResponse.Content.ReadAsStringAsync();
			DataServiceResponse<TId> serviceResponse = JsonSerializer
					.Deserialize<DataServiceResponse<TId>>(responseString, JsonSerializerOptions);
			return serviceResponse;
		}

		public virtual async Task<DataServiceResponse<TId>> DeleteAsync(TId id)
		{
			await LoadEntityControllerRoutes();
			HttpResponseMessage httpResponse = await HttpClient.DeleteAsync($"{EntityControllerRoute}/{id}");
			if (!httpResponse.IsSuccessStatusCode)
				return new DataServiceResponse<TId>(true, httpResponse.StatusCode.ToString());
			string responseString = await httpResponse.Content.ReadAsStringAsync();
			DataServiceResponse<TId> serviceResponse = JsonSerializer
					.Deserialize<DataServiceResponse<TId>>(responseString, JsonSerializerOptions);
			return serviceResponse;
		}

		protected async Task LoadEntityControllerRoutes()
		{
			if (string.IsNullOrEmpty(EntityControllerRoute))
				EntityControllerRoute = await entityControllerProvider.GetControllerRouteFor<TEntity>();
		}
	}
}
