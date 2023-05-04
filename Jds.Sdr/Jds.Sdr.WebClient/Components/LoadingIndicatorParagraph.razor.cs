using Microsoft.AspNetCore.Components;

namespace Jds.Sdr.WebClient.Components
{
	public partial class LoadingIndicatorParagraph
	{
		[Parameter]
		public int Count { get; set; } = 1;
	}
}
