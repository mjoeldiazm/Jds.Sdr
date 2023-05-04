using Microsoft.AspNetCore.Components;

namespace Jds.Sdr.WebClient.Shared
{
	public partial class Header
	{
		[Parameter] public bool ToggleOn { get; set; }
		[Parameter] public EventCallback<bool> ToggleOnChanged { get; set; }

		async Task OnToggleClick() => await Toggle();

		async Task Toggle(bool? value = null)
		{
			var newValue = value ?? !ToggleOn;
			if (ToggleOn != newValue)
			{
				ToggleOn = newValue;
				await ToggleOnChanged.InvokeAsync(ToggleOn);
			}
		}
	}
}
