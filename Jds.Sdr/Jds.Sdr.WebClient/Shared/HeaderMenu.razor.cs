using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Jds.Sdr.WebClient.Shared
{
	public partial class HeaderMenu
	{
		[CascadingParameter] protected Task<AuthenticationState> AuthenticationStateTask { get; set; } = null;

		protected AuthenticationState AuthenticationState { get; set; }

		protected override async Task OnInitializedAsync()
		{
			_ = await AuthenticationStateTask;
			await InvokeAsync(StateHasChanged);
		}
	}
}
