using Microsoft.AspNetCore.Components;

namespace Jds.Sdr.WebClient.Pages
{
	public partial class Configuration
	{
		[Inject]
		public NavigationManager NavigationManager { get; set; }
	}
}
