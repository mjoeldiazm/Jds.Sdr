using DevExpress.Blazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Jds.Sdr.WebClient.UI
{
	public class PopupMessageButton
	{
		public string Text { get; set; }

		public ButtonRenderStyle RenderStyle { get; set; }

		public EventCallback<MouseEventArgs> OnClickCallback { get; set; }
	}
}
