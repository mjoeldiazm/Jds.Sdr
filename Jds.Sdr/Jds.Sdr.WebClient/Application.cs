using System.Globalization;

namespace Jds.Sdr.WebClient
{
	public static class Application
	{
		public const string Title = "Sistema de Distancia Recorrida";
		public const string SimplifiedTitle = "SDR";
		public static CultureInfo FormatDefaultCulture { get; } = CultureInfo.GetCultureInfo("es-HN");
	}
}
