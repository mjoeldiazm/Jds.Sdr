using Jds.Sdr.Shared.Data.EntityViews;
using Jds.Sdr.WebClient.Components;
using Jds.Sdr.WebClient.Routing;

namespace Jds.Sdr.WebClient.Pages.DataManagers
{
	public partial class BranchTripManager
	{
		public static RoutableComponentInformation ComponentInformation
		{ get; } = new RoutableComponentInformation()
		{
			IconCssClass = "fas fa-van-shuttle",
			Title = "Viajes",
			SimplifiedTitle = "Viajes",
			EntitySingularName = "Viaje",
			Route = RouteAttributeReader.GetValue(typeof(BranchTripManager))
		};

		protected Type SelectedComponent { get; set; }
		protected ComponentParameter<TripView> TripParameter { get; set; } = new();

		protected override void OnInitialized()
		{
			SelectedComponent = typeof(TripManager);
			TripParameter.GoTo = GoTo;
		}

		protected void GoTo(Type destinationComponentType)
		{
			SelectedComponent = destinationComponentType;
			StateHasChanged();
		}
	}
}
