using Jds.Sdr.Shared.Interfaces;

namespace Jds.Sdr.Server.Controllers
{
	public class EntityOperationResult<TEntity> where TEntity : class, IDataEntity<int>
	{
		public TEntity Entity { get; set; }
		public bool IsSuccessful { get; set; }
		public string ResultMessage { get; set; }

		public EntityOperationResult(TEntity entity, bool isSuccessful, string resultMessage = "")
		{
			Entity = entity;
			IsSuccessful = isSuccessful;
			ResultMessage = resultMessage;
		}
	}
}
