using Jds.Sdr.Shared.Interfaces;
using Jds.Sdr.Shared.Services;

namespace Jds.Sdr.WebClient.Interfaces
{
	public interface IDataRepository<TEntity, TId> where TEntity : IDataEntity<TId>
	{
		Task<DataServiceResponse<IEnumerable<TEntity>>> GetAllAsync();

		Task<DataServiceResponse<TId>> PostAsync(TEntity objectToPost);

		Task<DataServiceResponse<TId>> PutAsync(TEntity objectToPut);

		Task<DataServiceResponse<TId>> DeleteAsync(TId id);
	}
}
