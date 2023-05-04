using Jds.Sdr.Shared.Data.EntityViews;
using Jds.Sdr.Shared.Services;
using Jds.Sdr.WebClient.Components;
using Jds.Sdr.WebClient.Interfaces;
using Jds.Sdr.WebClient.Routing;
using Jds.Sdr.WebClient.UI;
using Microsoft.AspNetCore.Components;

namespace Jds.Sdr.WebClient.Reports
{
	public partial class TripReport
	{
		public static RoutableComponentInformation ComponentInformation
		{ get; } = new RoutableComponentInformation()
		{
			IconCssClass = "fas fa-file-spreadsheet",
			Title = "Reporte de Transporte",
			SimplifiedTitle = "Reporte de Transporte",
			EntitySingularName = "Reporte de Transporte",
			Route = RouteAttributeReader.GetValue(typeof(TripReport))
		};

		[Inject]
		protected IDataRepository<TripView, int> TripsDataRepository { get; set; }

		[Inject]
		protected NavigationManager NavigationManager { get; set; }

		protected IEnumerable<TripView> Trips { get; set; }

		protected DateTime StartDate { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
		protected DateTime EndDate { get; set; } = DateTime.Now;
		protected CarrierView SelectedCarrier { get; set; }
		protected CarrierView ReportCarrier { get; set; }

		protected ToastMessage ToastMessage { get; set; }

		protected async Task GenerateReport()
		{
			if (SelectedCarrier is null)
			{
				await ToastMessage.ShowAsync(MessageType.Error,
					"Reporte de Transporte", "Seleccione un transportista.");
				return;
			}
			if (StartDate > EndDate)
			{
				await ToastMessage.ShowAsync(MessageType.Error,
					"Reporte de Transporte", "La fecha inicial no puede ser mayor que la final.");
				return;
			}
			ReportCarrier = SelectedCarrier;
			DataServiceResponse<IEnumerable<TripView>> serviceResponse;
			serviceResponse = await TripsDataRepository.GetAllAsync();
			if (!serviceResponse.HasError)
				Trips = serviceResponse.Response
					.Where(x => x.Date >= StartDate && x.Date <= EndDate
						&& x.CarrierId == SelectedCarrier.Id)
					.OrderBy(x => x.Date)
					.ToList();
		}
	}
}
