using Jds.Sdr.Server.Controllers;
using Jds.Sdr.Shared.Interfaces;
using Jds.Sdr.Shared.Services;

namespace Jds.Sdr.Server.Interfaces
{
	public interface IEntityController<TEntity, TView> where TEntity : class, IDataEntity<int>
	{
		Task<DataServiceResponse<IEnumerable<TView>>> GetAllAsync();

		Task<DataServiceResponse<int>> PostAsync(TView objectToPost);

		Task<DataServiceResponse<int>> PutAsync(TView objectToPut);

		Task<DataServiceResponse<int>> DeleteAsync(int id);

		Task<EntityOperationResult<TEntity>> BeforePost(TEntity objectToPost);

		Task<EntityOperationResult<TEntity>> AfterPost(TEntity objectToPost);
	}
}
