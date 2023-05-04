using Jds.Sdr.Shared.Data.EntityViews;
using Jds.Sdr.WebClient.Components;
using Jds.Sdr.WebClient.Routing;
using Jds.Sdr.WebClient.UI;

namespace Jds.Sdr.WebClient.Pages.DataManagers
{
	partial class CarrierManager
	{
		public static RoutableComponentInformation ComponentInformation
		{ get; } = new RoutableComponentInformation()
		{
			IconCssClass = "fas fa-steering-wheel",
			Title = "Transportistas",
			SimplifiedTitle = "Transportistas",
			EntitySingularName = "Transportista",
			Route = RouteAttributeReader.GetValue(typeof(CarrierManager))
		};

		protected override async Task OnModelOperationErrorDetected(CarrierView model, string message)
		{
			await ToastMessage.ShowAsync(MessageType.Error,
				DialogHeaderTexts.ValidationError,
				string.Format("'{0}': {1}", model.EmailAddress, message));
		}

		protected override void OnSelectedModelChanged(CarrierView model)
		{
			CanDeleteSelectedModel = !AuthenticationState.User.Identity.Name.Equals(model.EmailAddress);
			base.OnSelectedModelChanged(model);
		}
	}
}
