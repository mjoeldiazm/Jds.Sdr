using DevExpress.Blazor;
using Jds.Sdr.WebClient.Routing;
using Microsoft.AspNetCore.Components;

namespace Jds.Sdr.WebClient.Components
{
	public class DataManagerTreeViewNode : DxTreeViewNode
	{
		[Parameter]
		public RoutableComponentInformation ComponentInformation { get; set; }

		protected override void OnParametersSet()
		{
			if (ComponentInformation is not null)
			{
				NavigateUrl = ComponentInformation.Route;
				Text = ComponentInformation.Title;
				IconCssClass = ComponentInformation.IconCssClass;
				CssClass = "text-primary";
			}
		}
	}
}
