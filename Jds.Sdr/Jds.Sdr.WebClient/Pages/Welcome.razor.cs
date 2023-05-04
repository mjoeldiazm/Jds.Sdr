using Microsoft.AspNetCore.Components;

namespace Jds.Sdr.WebClient.Pages
{
	public partial class Welcome
	{
		[Inject]
		protected NavigationManager NavigationManager { get; set; }
	}
}
