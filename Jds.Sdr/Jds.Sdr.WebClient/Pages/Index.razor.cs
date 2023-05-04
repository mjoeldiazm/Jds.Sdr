using Microsoft.AspNetCore.Components;

namespace Jds.Sdr.WebClient.Pages
{
	public partial class Index
	{
		[Inject]
		protected NavigationManager NavigationManager { get; set; }
	}
}
