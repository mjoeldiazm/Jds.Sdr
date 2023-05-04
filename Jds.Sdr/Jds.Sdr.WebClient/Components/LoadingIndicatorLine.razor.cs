using Jds.Sdr.WebClient.UI;
using Microsoft.AspNetCore.Components;

namespace Jds.Sdr.WebClient.Components
{
	public partial class LoadingIndicatorLine
	{
		[Parameter]
		[EditorRequired]
		public LoadingLineWidth Width { get; set; }
	}
}
