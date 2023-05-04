using Jds.Sdr.Shared.Data.EntityViews;
using Jds.Sdr.Shared.Services;
using Jds.Sdr.WebClient.Interfaces;
using Microsoft.AspNetCore.Components;

namespace Jds.Sdr.WebClient.Components
{
	public partial class TripReportToolbar
	{
		[Inject]
		protected IDataRepository<CarrierView, int> CarrierDataRepository { get; set; }

		[Parameter]
		public DateTime StartDate { get; set; }

		[Parameter]
		public DateTime EndDate { get; set; }

		[Parameter]
		public CarrierView Carrier { get; set; }

		[Parameter]
		public EventCallback<DateTime> StartDateChanged { get; set; }

		[Parameter]
		public EventCallback<DateTime> EndDateChanged { get; set; }

		[Parameter]
		public EventCallback<CarrierView> CarrierChanged { get; set; }

		[Parameter]
		public EventCallback GenerateClicked { get; set; }

		protected IEnumerable<CarrierView> Carriers { get; set; }

		protected override async Task OnInitializedAsync()
		{
			DataServiceResponse<IEnumerable<CarrierView>> serviceResponse;
			serviceResponse = await CarrierDataRepository.GetAllAsync();
			if (!serviceResponse.HasError)
				Carriers = serviceResponse.Response
					.OrderBy(x => x.FullName);
		}

		protected async Task OnStartDateChanged(DateTime startDate)
		{
			StartDate = startDate;
			await StartDateChanged.InvokeAsync(StartDate);
		}

		protected async Task OnEndDateChanged(DateTime endDate)
		{
			EndDate = endDate;
			await EndDateChanged.InvokeAsync(EndDate);
		}

		protected async Task OnCarrierChanged(CarrierView carrier)
		{
			Carrier = carrier;
			await CarrierChanged.InvokeAsync(Carrier);
		}

		protected async Task OnGenerateClicked()
		{
			await GenerateClicked.InvokeAsync();
		}

	}
}
