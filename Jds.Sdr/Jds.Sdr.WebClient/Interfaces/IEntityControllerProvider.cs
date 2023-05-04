namespace Jds.Sdr.WebClient.Interfaces
{
	public interface IEntityControllerProvider
	{
		Task<string> GetControllerRouteFor<TEntity>();
	}
}
