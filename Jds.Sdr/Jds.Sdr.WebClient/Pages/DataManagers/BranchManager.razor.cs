using Jds.Sdr.Shared.Data.EntityViews;
using Jds.Sdr.WebClient.Components;
using Jds.Sdr.WebClient.Routing;
using Jds.Sdr.WebClient.UI;

namespace Jds.Sdr.WebClient.Pages.DataManagers
{
	partial class BranchManager
	{
		public static RoutableComponentInformation ComponentInformation
		{ get; } = new RoutableComponentInformation()
		{
			IconCssClass = "fas fa-store",
			Title = "Sucursales",
			SimplifiedTitle = "Sucursales",
			EntitySingularName = "Sucursal",
			Route = RouteAttributeReader.GetValue(typeof(BranchManager))
		};

		protected override async Task OnModelOperationErrorDetected(BranchView model, string message)
		{
			await ToastMessage.ShowAsync(MessageType.Error,
				DialogHeaderTexts.ValidationError,
				string.Format("'{0}': {1}", model.Name, message));
		}
	}
}
