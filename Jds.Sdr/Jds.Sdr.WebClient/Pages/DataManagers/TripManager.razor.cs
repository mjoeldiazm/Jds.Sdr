using Jds.Sdr.Shared.Data.EntityViews;
using Jds.Sdr.Shared.Security;
using Jds.Sdr.Shared.Services;
using Jds.Sdr.WebClient.Components;
using Jds.Sdr.WebClient.Interfaces;
using Jds.Sdr.WebClient.Routing;
using Jds.Sdr.WebClient.UI;
using Microsoft.AspNetCore.Components;

namespace Jds.Sdr.WebClient.Pages.DataManagers
{
	partial class TripManager
	{
		public static RoutableComponentInformation ComponentInformation
		{ get; } = new RoutableComponentInformation()
		{
			IconCssClass = "fas fa-van-shuttle",
			Title = "Viajes",
			SimplifiedTitle = "Viajes",
			EntitySingularName = "Viaje",
			Route = RouteAttributeReader.GetValue(typeof(TripManager))
		};

		[Inject]
		protected IDataRepository<BranchView, int> BranchesDataRepository { get; set; }

		[Inject]
		protected IDataRepository<CarrierView, int> CarriersDataRepository { get; set; }

		[Inject]
		protected IDataRepository<DistanceView, int> BranchEmployeesDataRepository { get; set; }

		[CascadingParameter]
		public ComponentParameter<TripView> TripParameter { get; set; }

		protected IEnumerable<BranchView> Branches { get; set; }
		protected IEnumerable<CarrierView> Carriers { get; set; }
		protected IEnumerable<DistanceView> BranchEmployees { get; set; }
		protected bool CurrentUserIsAdministratorOrStoreManager
		{
			get => AuthenticationState.User.IsInRole(RoleDefinitions.Administrator)
				|| AuthenticationState.User.IsInRole(RoleDefinitions.StoreManager);
		}

		protected override async Task LoadRelatedDataAsync()
		{
			uint errorCounter = 0;
			errorCounter += Convert.ToUInt32(await LoadBranches());
			errorCounter += Convert.ToUInt32(await LoadCarriers());
			if (errorCounter > 0)
				await ToastMessage.ShowAsync(MessageType.Error,
					DialogHeaderTexts.DataService,
					DialogMessages.DataRetrieveError);
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

		protected async Task<bool> LoadCarriers()
		{
			if (Carriers is null)
			{
				DataServiceResponse<IEnumerable<CarrierView>> serviceResponse;
				serviceResponse = await CarriersDataRepository.GetAllAsync();
				if (!serviceResponse.HasError)
					Carriers = serviceResponse.Response
						.OrderBy(x => x.FullName);
				return serviceResponse.HasError;
			}
			return false;
		}

		protected override Task OnAddEntity(TripView model)
		{
			EditContext.DataItem.Date = DateTime.Now.Date;
			return Task.CompletedTask;
		}

		protected void GoToCaseActivityManager()
		{
			TripParameter.Value = SelectedModel;
			TripParameter.GoTo(typeof(TripDistanceManager));
		}
	}
}
