using Jds.Sdr.WebClient.UI;
using Jds.Sdr.WebClient.Web;
using Jds.Sdr.WebClient.Web.JS;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Jds.Sdr.WebClient.Components
{
	partial class ToastMessage
	{
		private string iconClass;
		private IJSObjectReference jsUIModule;

		[Inject]
		protected IJSRuntime JSRuntime { get; set; }

		protected string Name { get; set; } = Guid.NewGuid().ToString("N");
		protected string HeaderText { get; set; }
		protected ThemeColor HeaderTextColor { get; set; } = ThemeColor.White;
		protected ThemeColor HeaderColor { get; set; } = ThemeColor.White;
		protected ThemeColor BodyColor { get; set; } = ThemeColor.Muted;
		protected ThemeColor BodyTextColor { get; set; } = ThemeColor.White;
		protected string Message { get; set; }
		protected PopupMessageIcon Icon { get; set; }

		protected bool CloseButtonVisible { get; set; } = true;

		protected string IconClass
		{
			get
			{
				if (Icon == PopupMessageIcon.None)
					iconClass = string.Empty;
				if (Icon == PopupMessageIcon.Information)
					iconClass = "fas fa-info-circle";
				if (Icon == PopupMessageIcon.Warning)
					iconClass = "fas fa-exclamation-circle";
				if (Icon == PopupMessageIcon.Question)
					iconClass = "fas fa-question-circle";
				if (Icon == PopupMessageIcon.Error)
					iconClass = "fas fa-times-circle";
				return iconClass;
			}
		}

		protected async override Task OnInitializedAsync()
		{
			jsUIModule = await JSRuntime.InvokeAsync<IJSObjectReference>(
				JSImportedFunctionNames.Import,
				StaticResourceReference.Build(WebResourceType.JavaScriptFile,
				JSResourceReferences.ToastMessageScript));
		}

		public async Task ShowAsync(MessageType messageType, string headerText, string message)
		{
			Message = message;
			HeaderText = headerText;
			SetAppearanceByMessageType(messageType);
			await InvokeAsync(StateHasChanged);
			await InvokeToastMessageAsync();
		}

		protected void SetAppearanceByMessageType(MessageType messageType)
		{
			switch (messageType)
			{
				case MessageType.Information:
					HeaderColor = ThemeColor.Primary;
					BodyColor = ThemeColor.Primary;
					Icon = PopupMessageIcon.Information;
					break;
				case MessageType.Warning:
					HeaderColor = ThemeColor.Warning;
					BodyColor = ThemeColor.Warning;
					Icon = PopupMessageIcon.Warning;
					break;
				case MessageType.Question:
					HeaderColor = ThemeColor.Primary;
					BodyColor = ThemeColor.Primary;
					Icon = PopupMessageIcon.Question;
					break;
				case MessageType.Error:
					HeaderColor = ThemeColor.Danger;
					BodyColor = ThemeColor.Danger;
					Icon = PopupMessageIcon.Error;
					break;
			}
		}

		protected async Task InvokeToastMessageAsync()
		{
			if (jsUIModule is not null)
				await jsUIModule.InvokeVoidAsync(JSImportedFunctionNames.ShowToastMessage, Name);
		}
	}
}
