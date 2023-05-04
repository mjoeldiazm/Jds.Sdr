namespace Jds.Sdr.WebClient.Web
{
	internal static class StaticResourceReference
	{
		private const string ResourcePath = "/";

		public static string Build(WebResourceType type, string resourceName = "")
		{
			if (type == WebResourceType.Image)
				return $"{ResourcePath}{resourceName}";
			if (type == WebResourceType.JavaScriptFile)
				return $"{ResourcePath}{resourceName}";
			return string.Empty;
		}
	}
}
