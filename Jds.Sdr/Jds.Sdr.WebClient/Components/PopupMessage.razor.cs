using DevExpress.Blazor;
using Jds.Sdr.WebClient.UI;
using Microsoft.AspNetCore.Components;

namespace Jds.Sdr.WebClient.Components
{
	public partial class PopupMessage
	{
		[Parameter]
		public string HeaderText { get; set; }

		[Parameter]
		public ThemeColor HeaderColor { get; set; }

		[Parameter]
		public PopupMessageButtonDefaultText ButtonDefaultText { get; set; }

		[Parameter]
		public ThemeColor HeaderTextColor { get; set; }

		[Parameter]
		public string Message { get; set; }

		[Parameter]
		public PopupMessageIcon Icon { get; set; }

		[Parameter]
		public bool Visible { get; set; }

		[Parameter]
		public List<PopupMessageButton> Buttons { get; set; }

		[Parameter]
		public PopupMessageButtonSource ButtonSource { get; set; }

		[Parameter]
		public Alignment ButtonAlignment { get; set; }

		[Parameter]
		public string DefaultFirstButtontText { get; set; }

		[Parameter]
		public ButtonRenderStyle DefaultFirstButtonRenderStyle { get; set; }

		[Parameter]
		public EventCallback DefaultFirstButtonOnClick { get; set; }

		[Parameter]
		public string DefaultSecondButtonText { get; set; }

		[Parameter]
		public ButtonRenderStyle DefaultSecondButtonRenderStyle { get; set; }

		[Parameter]
		public EventCallback DefaultSecondButtonOnClick { get; set; }

		[Parameter]
		public string DefaultThirdButtonText { get; set; }

		[Parameter]
		public ButtonRenderStyle DefaultThirdButtonRenderStyle { get; set; }

		[Parameter]
		public EventCallback DefaultThirdButtonOnClick { get; set; }

		protected string IconCssClass { get; private set; }

		private string DialogCancel
		{
			get => DialogButtonTexts.Cancel;
		}

		private string DialogAccept
		{
			get => DialogButtonTexts.Accept;
		}

		private string DialogOk
		{
			get => DialogButtonTexts.Accept;
		}

		private string DialogYes
		{
			get => DialogButtonTexts.Yes;
		}

		private string DialogNo
		{
			get => DialogButtonTexts.No;
		}

		protected override Task OnInitializedAsync()
		{
			HeaderColor = ThemeColor.Primary;
			ButtonDefaultText = PopupMessageButtonDefaultText.Ok;
			HeaderTextColor = ThemeColor.White;
			Icon = PopupMessageIcon.None;
			ButtonSource = PopupMessageButtonSource.Default;
			ButtonAlignment = Alignment.End;
			DefaultFirstButtonRenderStyle = ButtonRenderStyle.Primary;
			DefaultSecondButtonRenderStyle = ButtonRenderStyle.Secondary;
			DefaultThirdButtonRenderStyle = ButtonRenderStyle.Secondary;
			return base.OnInitializedAsync();
		}

		protected override void OnParametersSet()
		{
			SetButtonDefaultText();
			SetIconCssClass();
		}

		private void SetIconCssClass()
		{
			switch (Icon)
			{
				case PopupMessageIcon.None:
					IconCssClass = string.Empty;
					break;
				case PopupMessageIcon.Information:
					IconCssClass = "fas fa-info-circle";
					break;
				case PopupMessageIcon.Warning:
					IconCssClass = "fas fa-exclamation-circle";
					break;
				case PopupMessageIcon.Question:
					IconCssClass = "fas fa-question-circle";
					break;
				case PopupMessageIcon.Error:
					IconCssClass = "fas fa-times-circle";
					break;
			}
		}

		private void SetButtonDefaultText()
		{
			switch (ButtonDefaultText)
			{
				case PopupMessageButtonDefaultText.Ok:
					DefaultFirstButtontText = DialogOk;
					DefaultSecondButtonText = string.Empty;
					DefaultThirdButtonText = string.Empty;
					break;
				case PopupMessageButtonDefaultText.YesNo:
					DefaultFirstButtontText = DialogYes;
					DefaultSecondButtonText = DialogNo;
					DefaultThirdButtonText = string.Empty;
					break;
				case PopupMessageButtonDefaultText.YesNoCancel:
					DefaultFirstButtontText = DialogYes;
					DefaultSecondButtonText = DialogNo;
					DefaultThirdButtonText = DialogCancel;
					break;
				case PopupMessageButtonDefaultText.Accept:
					DefaultFirstButtontText = DialogAccept;
					DefaultSecondButtonText = string.Empty;
					DefaultThirdButtonText = string.Empty;
					break;
				case PopupMessageButtonDefaultText.AcceptCancel:
					DefaultFirstButtontText = DialogAccept;
					DefaultSecondButtonText = DialogCancel;
					DefaultThirdButtonText = string.Empty;
					break;
			}
		}
	}
}
