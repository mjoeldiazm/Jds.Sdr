using Microsoft.AspNetCore.Components;

namespace Jds.Sdr.WebClient.Routing
{
	public static class RouteAttributeReader
	{
		public static string GetValue(Type type)
		{
			RouteAttribute attribute = type.GetCustomAttributes(typeof(RouteAttribute), true)
				.FirstOrDefault() as RouteAttribute;
			return attribute?.Template ?? string.Empty;
		}
	}
}
