using Jds.Sdr.Shared.Security;

namespace Jds.Sdr.WebClient.Interfaces
{
	public interface ILoginService
	{
		Task Login(UserAccountLoginInformation loginInformation);
		Task Logout();
		Task RenewTokenHandler();
	}
}
