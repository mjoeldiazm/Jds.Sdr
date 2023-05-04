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
	partial class TripDistanceManager
	{
		public static RoutableComponentInformation ComponentInformation
		{ get; } = new RoutableComponentInformation()
		{
			IconCssClass = "fas fa-user-group-simple",
			Title = "Viajes",
			SimplifiedTitle = "Viajes",
			EntitySingularName = "Empleado",
			Route = RouteAttributeReader.GetValue(typeof(TripDistanceManager))
		};

		[CascadingParameter]
		public ComponentParameter<TripView> TripParameter { get; set; }

		[Inject]
		protected IDataRepository<DistanceView, int> DistancesDataRepository { get; set; }

		protected TripView SelectedTrip { get; set; }
		protected IEnumerable<DistanceView> Distances { get; set; }

		protected bool CurrentUserIsAdministratorOrStoreManager
		{
			get => AuthenticationState.User.IsInRole(RoleDefinitions.Administrator)
				|| AuthenticationState.User.IsInRole(RoleDefinitions.StoreManager);
		}

		protected override void OnInitialized()
		{
			SelectedTrip = TripParameter.Value;
		}

		protected override Task<IEnumerable<TripDistanceView>> FilterDataResponse(IEnumerable<TripDistanceView> entities)
		{
			return Task.FromResult(entities.Where(x => x.TripId.Equals(SelectedTrip.Id)));
		}

		protected override async Task LoadRelatedDataAsync()
		{
			if (Distances is null)
			{
				DataServiceResponse<IEnumerable<DistanceView>> serviceResponse;
				serviceResponse = await DistancesDataRepository.GetAllAsync();
				if (!serviceResponse.HasError)
					Distances = serviceResponse.Response
						.Where(x => x.BranchId == SelectedTrip.BranchId)
						.OrderBy(x => x.Employee.FullName);
			}
		}

		protected override async Task OnModelOperationErrorDetected(TripDistanceView model, string message)
		{
			await ToastMessage.ShowAsync(MessageType.Error, DialogHeaderTexts.ValidationError,
				string.Format(message));
		}

		protected override Task OnAddEntity(TripDistanceView model)
		{
			EditContext.DataItem.TripId = SelectedTrip.Id;
			EditContext.DataItem.Trip = SelectedTrip;
			return Task.CompletedTask;
		}

		protected void GoToTripManager()
		{
			TripParameter.GoTo.Invoke(typeof(TripManager));
		}
	}
}
