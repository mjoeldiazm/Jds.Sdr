namespace Jds.Sdr.WebClient.UI
{
	public enum LoadingLineWidth : int
	{
		Percent25 = 25,
		Percent50 = 50,
		Percent75 = 75,
		Percent100 = 100,
	}
	public enum Alignment : int
	{
		End = 0,
		Center = 1,
		Start = 2,
	}

	public enum PopupMessageButtonDefaultText : int
	{
		Ok = 0,
		YesNo = 1,
		YesNoCancel = 2,
		Accept = 3,
		AcceptCancel = 5
	}

	public enum ThemeColor : int
	{
		Primary = 0,
		Secondary = 1,
		Success = 2,
		Danger = 3,
		Warning = 4,
		Info = 5,
		Light = 6,
		Dark = 7,
		Body = 8,
		Muted = 9,
		White = 10,
		Black50 = 11,
		White50 = 12
	}

	public enum PopupMessageButtonSource : int
	{
		Default = 0,
		UserDefined = 1
	}

	public enum PopupMessageIcon : int
	{
		None = 0,
		Information = 1,
		Warning = 2,
		Question = 3,
		Error = 4,
	}

	public enum MessageType : int
	{
		Information = 0,
		Warning = 1,
		Question = 2,
		Error = 3,
	}
}
