using Jds.Sdr.Shared.Data.EntityViews;
using Jds.Sdr.Shared.Security;
using Jds.Sdr.Shared.Services;

namespace Jds.Sdr.Shared.Interfaces
{
	public interface IUserAccountLogin
	{
		Task<DataServiceResponse<UserAccountLoginInformation>> Login(UserAccountCredential userAccountCredential);
		Task<DataServiceResponse<UserAccountTokenView>> RenewToken();
	}
}
