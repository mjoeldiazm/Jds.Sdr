using Jds.Sdr.Shared.Data.EntityViews;
using Jds.Sdr.WebClient.Components;
using Jds.Sdr.WebClient.Routing;
using Jds.Sdr.WebClient.UI;

namespace Jds.Sdr.WebClient.Pages.DataManagers
{
	partial class EmployeeManager
	{
		public static RoutableComponentInformation ComponentInformation
		{ get; } = new RoutableComponentInformation()
		{
			IconCssClass = "fas fa-user-tie",
			Title = "Colaboradores",
			SimplifiedTitle = "Colaboradores",
			EntitySingularName = "Colaborador",
			Route = RouteAttributeReader.GetValue(typeof(EmployeeManager))
		};

		protected override async Task OnModelOperationErrorDetected(EmployeeView model, string message)
		{
			await ToastMessage.ShowAsync(MessageType.Error,
				DialogHeaderTexts.ValidationError,
				string.Format("'{0}': {1}", model.EmailAddress, message));
		}

		protected override void OnSelectedModelChanged(EmployeeView model)
		{
			CanDeleteSelectedModel = !AuthenticationState.User.Identity.Name.Equals(model.EmailAddress);
			base.OnSelectedModelChanged(model);
		}
	}
}
