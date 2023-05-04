using Jds.Sdr.WebClient.Pages.Authentication;
using Jds.Sdr.WebClient.Routing;
using Jds.Sdr.WebClient.Security;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.JSInterop;

namespace Jds.Sdr.WebClient.Shared
{
	public partial class MainLayout
	{
		bool isMobileLayout;
		bool isSidebarExpanded = true;

		[Inject] UserAccountTokenRenewer UserAccountTokenRenewer { get; set; }
		[Inject] IJSRuntime JSRuntime { get; set; }

		[CascadingParameter] Task<AuthenticationState> AuthenticationStateTask { get; set; } = null;

		string NavMenuCssClass { get; set; }

		bool IsMobileLayout
		{
			get => isMobileLayout;
			set
			{
				isMobileLayout = value;
				IsSidebarExpanded = !isMobileLayout;
			}
		}

		bool IsSidebarExpanded
		{
			get => isSidebarExpanded;
			set
			{
				if (isSidebarExpanded != value)
				{
					NavMenuCssClass = value ? "expand" : "collapse";
					isSidebarExpanded = value;
				}
			}
		}

		protected async override Task OnInitializedAsync()
		{
			NavigationManager.LocationChanged += OnLocationChanged;
			await JSRuntime.InvokeVoidAsync("inactiveTimer", DotNetObjectReference.Create(this));
			UserAccountTokenRenewer.Initialize();
		}

		[JSInvokable]
		public async Task Logout()
		{
			AuthenticationState authenticationState = await AuthenticationStateTask;
			if (authenticationState.User.Identity.IsAuthenticated)
			{
				NavigationManager.NavigateTo(RouteAttributeReader.GetValue(typeof(Logout)));
			}
		}

		async void OnLocationChanged(object sender, LocationChangedEventArgs args)
		{
			if (IsMobileLayout)
			{
				IsSidebarExpanded = false;
				await InvokeAsync(StateHasChanged);
			}
		}

		public void Dispose()
		{
			NavigationManager.LocationChanged -= OnLocationChanged;
		}
	}
}
