using Jds.Sdr.Shared.Data.EntityViews;
using Jds.Sdr.Shared.Services;
using Jds.Sdr.WebClient.Components;
using Jds.Sdr.WebClient.Interfaces;
using Jds.Sdr.WebClient.Routing;
using Jds.Sdr.WebClient.UI;
using Microsoft.AspNetCore.Components;

namespace Jds.Sdr.WebClient.Pages.DataManagers
{
	partial class UserAccountManager
	{
		public static RoutableComponentInformation ComponentInformation
		{ get; } = new RoutableComponentInformation()
		{
			IconCssClass = "fas fa-users",
			Title = "Cuentas de Usuario",
			SimplifiedTitle = "Cuentas de Usuario",
			EntitySingularName = "Cuenta de Usuario",
			Route = RouteAttributeReader.GetValue(typeof(UserAccountManager))
		};

		[Inject]
		protected IDataRepository<EmployeeView, int> EmployeesDataRepository { get; set; }
		[Inject]
		protected IDataRepository<RoleView, int> RolesDataRepository { get; set; }

		protected IEnumerable<EmployeeView> Employees { get; set; }
		protected IEnumerable<RoleView> Roles { get; set; }

		protected override async Task LoadRelatedDataAsync()
		{
			uint errorCounter = 0;
			errorCounter += Convert.ToUInt32(await LoadEmployees());
			errorCounter += Convert.ToUInt32(await LoadRoles());
			if (errorCounter > 0)
				await ToastMessage.ShowAsync(MessageType.Error,
					DialogHeaderTexts.DataService,
					DialogMessages.DataRetrieveError);
		}

		protected async Task<bool> LoadEmployees()
		{
			if (Employees is null)
			{
				DataServiceResponse<IEnumerable<EmployeeView>> serviceResponse;
				serviceResponse = await EmployeesDataRepository.GetAllAsync();
				if (!serviceResponse.HasError)
					Employees = serviceResponse.Response
						.OrderBy(x => x.FullName);
				return serviceResponse.HasError;
			}
			return false;
		}

		protected async Task<bool> LoadRoles()
		{
			if (Roles is null)
			{
				DataServiceResponse<IEnumerable<RoleView>> serviceResponse;
				serviceResponse = await RolesDataRepository.GetAllAsync();
				if (!serviceResponse.HasError)
					Roles = serviceResponse.Response
						.OrderBy(x => x.Name);
				return serviceResponse.HasError;
			}
			return false;
		}

		protected override Task<bool> IsValidModelToInsertAsync()
		{
			return base.IsValidModelToInsertAsync();
		}

		protected override Task OnEditEntity(UserAccountView model)
		{
			EditContext.DataItem.Employee = null;
			return Task.CompletedTask;
		}

		protected override async Task OnModelOperationErrorDetected(UserAccountView model, string message)
		{
			await ToastMessage.ShowAsync(MessageType.Error,
				DialogHeaderTexts.ValidationError,
				string.Format("'{0}': {1}", model.EmailAddress, message));
		}

		protected override void OnSelectedModelChanged(UserAccountView model)
		{
			CanEditSelectedModel = !AuthenticationState.User.Identity.Name.Equals(model.EmailAddress);
			CanDeleteSelectedModel = !AuthenticationState.User.Identity.Name.Equals(model.EmailAddress);
			base.OnSelectedModelChanged(model);
		}

		protected void SelectedEmployeeChanged(EmployeeView employee)
		{
			EditContext.DataItem.EmailAddress = employee.EmailAddress;
			EditContext.DataItem.PhoneNumber = employee.PhoneNumber;
		}
	}
}
