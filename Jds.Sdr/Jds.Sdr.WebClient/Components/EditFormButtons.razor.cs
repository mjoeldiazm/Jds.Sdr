using DevExpress.Blazor;
using Jds.Sdr.WebClient.Data;
using Jds.Sdr.WebClient.UI;
using Microsoft.AspNetCore.Components;

namespace Jds.Sdr.WebClient.Components
{
	public partial class EditFormButtons
	{
		[Parameter]
		public Alignment Alignment { get; set; }

		[Parameter]
		public bool SubmitFormOnClick { get; set; }

		[Parameter]
		public EventCallback OnClickCancelButton { get; set; }

		[Parameter]
		public ButtonRenderStyle PrimaryButtonRenderStyle { get; set; }

		[Parameter]
		public ButtonRenderStyle CancelButtonRenderStyle { get; set; }

		[Parameter]
		public DataOperation DataOperation { get; set; }

		[Parameter]
		public bool IsSavingChanges { get; set; }

		protected override Task OnInitializedAsync()
		{
			Alignment = Alignment.End;
			SubmitFormOnClick = true;
			IsSavingChanges = false;
			PrimaryButtonRenderStyle = ButtonRenderStyle.Primary;
			CancelButtonRenderStyle = ButtonRenderStyle.Secondary;
			return base.OnInitializedAsync();
		}

		private async Task OnPrimaryClick()
		{
			await Task.CompletedTask;
		}

		private async Task OnCancelClick()
		{
			await OnClickCancelButton.InvokeAsync();
		}
	}
}
