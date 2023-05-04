using Jds.Sdr.WebClient.UI;
using Microsoft.AspNetCore.Components;

namespace Jds.Sdr.WebClient.Components
{
	public partial class IndicatorGlyph
	{
		[Parameter]
		[EditorRequired]
		public string IconCssClass { get; set; }

		[Parameter]
		[EditorRequired]
		public string Message { get; set; }

		[Parameter]
		public ThemeColor TextColor { get; set; } = ThemeColor.Primary;
	}
}
