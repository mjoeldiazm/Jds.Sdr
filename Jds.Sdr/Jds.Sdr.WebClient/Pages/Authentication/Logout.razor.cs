using Jds.Sdr.WebClient.Interfaces;
using Jds.Sdr.WebClient.Routing;
using Microsoft.AspNetCore.Components;

namespace Jds.Sdr.WebClient.Pages.Authentication
{
	public partial class Logout
	{
		[Inject]
		protected ILoginService LoginService { get; set; }
		[Inject]
		protected NavigationManager NavigationManager { get; set; }

		protected async override Task OnInitializedAsync()
		{
			await Task.Delay(2000);
			await LoginService.Logout();
			NavigationManager.NavigateTo(RouteAttributeReader.GetValue(typeof(Login)));
		}
	}
}
