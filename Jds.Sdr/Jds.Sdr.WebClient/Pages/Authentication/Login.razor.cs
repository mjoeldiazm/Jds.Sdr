using Jds.Sdr.Shared.Interfaces;
using Jds.Sdr.Shared.Security;
using Jds.Sdr.Shared.Services;
using Jds.Sdr.WebClient.Components;
using Jds.Sdr.WebClient.Interfaces;
using Jds.Sdr.WebClient.Routing;
using Jds.Sdr.WebClient.UI;
using Microsoft.AspNetCore.Components;

namespace Jds.Sdr.WebClient.Pages.Authentication
{
	public partial class Login
	{
		[Inject]
		protected IUserAccountLogin UserAccountLogin { get; set; }
		[Inject]
		protected ILoginService LoginService { get; set; }
		[Inject]
		protected NavigationManager NavigationManager { get; set; }

		protected UserAccountCredential Credentials { get; set; } = new();
		protected ToastMessage ToastMessage { get; set; }
		protected bool IsValidatingCredentials { get; set; } = false;

		protected bool IsExtraLargeScreen { get; set; }
		protected bool IsLargeScreen { get; set; }

		protected async Task OnValidSubmit()
		{
			IsValidatingCredentials = true;
			DataServiceResponse<UserAccountLoginInformation> serviceResponse = await UserAccountLogin
				.Login(Credentials);
			if (serviceResponse.HasError)
				await ToastMessage.ShowAsync(MessageType.Error, DialogHeaderTexts.Login, serviceResponse.Message);
			else
			{
				await LoginService.Login(serviceResponse.Response);
				NavigationManager.NavigateTo(RouteAttributeReader.GetValue(typeof(Welcome)));
			}
			IsValidatingCredentials = false;
		}
	}
}
