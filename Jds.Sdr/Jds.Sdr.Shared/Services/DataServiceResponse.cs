namespace Jds.Sdr.Shared.Services
{
	public class DataServiceResponse<T>
	{
		public bool HasError { get; set; }

		public T Response { get; set; }

		public string Message { get; set; }

		public int AffedtedRegisters { get; set; }

		public DataServiceResponse() { }

		public DataServiceResponse(T response)
		{
			HasError = false;
			Response = response;
			Message = String.Empty;
			AffedtedRegisters = 0;
		}

		public DataServiceResponse(bool hasError, string message)
		{
			HasError = hasError;
			Response = default;
			AffedtedRegisters = 0;
			Message = message;
		}

		public DataServiceResponse(bool hasError, int affectedRegisters, T response)
		{
			HasError = hasError;
			Response = response;
			AffedtedRegisters = affectedRegisters;
			Message = string.Empty;
		}

		public DataServiceResponse(bool hasError, int affectedRegisters, T response, string message)
		{
			HasError = hasError;
			Response = response;
			AffedtedRegisters = affectedRegisters;
			Message = message;
		}

		public DataServiceResponse(int affectedRegisters, T response, string message)
		{
			HasError = false;
			Response = response;
			AffedtedRegisters = affectedRegisters;
			Message = message;
		}
	}
}
