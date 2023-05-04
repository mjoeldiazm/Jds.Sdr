using Jds.Sdr.Shared.Data.EntityViews;
using Jds.Sdr.Shared.Services;
using Jds.Sdr.WebClient.Components;
using Jds.Sdr.WebClient.Interfaces;
using Jds.Sdr.WebClient.Routing;
using Jds.Sdr.WebClient.UI;
using Microsoft.AspNetCore.Components;

namespace Jds.Sdr.WebClient.Pages.DataManagers
{
	partial class DistanceManager
	{
		public static RoutableComponentInformation ComponentInformation
		{ get; } = new RoutableComponentInformation()
		{
			IconCssClass = "fas fa-people-roof",
			Title = "Empleados de Sucursales",
			SimplifiedTitle = "Empleados de Sucursales",
			EntitySingularName = "Empleado de Sucursal",
			Route = RouteAttributeReader.GetValue(typeof(DistanceManager))
		};

		[Inject]
		protected IDataRepository<BranchView, int> BranchesDataRepository { get; set; }
		[Inject]
		protected IDataRepository<EmployeeView, int> EmployeesDataRepository { get; set; }

		protected IEnumerable<BranchView> Branches { get; set; }
		protected IEnumerable<EmployeeView> Employees { get; set; }

		protected override async Task LoadRelatedDataAsync()
		{
			uint errorCounter = 0;
			errorCounter += Convert.ToUInt32(await LoadBranches());
			errorCounter += Convert.ToUInt32(await LoadEmployees());
			if (errorCounter > 0)
				await ToastMessage.ShowAsync(MessageType.Error,
					DialogHeaderTexts.DataService,
					DialogMessages.DataRetrieveError);
		}

		protected override async Task OnModelOperationErrorDetected(DistanceView model, string message)
		{
			await ToastMessage.ShowAsync(MessageType.Error, DialogHeaderTexts.ValidationError,
				string.Format(message));
		}

		protected async Task<bool> LoadBranches()
		{
			if (Branches is null)
			{
				DataServiceResponse<IEnumerable<BranchView>> serviceResponse;
				serviceResponse = await BranchesDataRepository.GetAllAsync();
				if (!serviceResponse.HasError)
					Branches = serviceResponse.Response
						.OrderBy(x => x.Name);
				return serviceResponse.HasError;
			}
			return false;
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
	}
}
