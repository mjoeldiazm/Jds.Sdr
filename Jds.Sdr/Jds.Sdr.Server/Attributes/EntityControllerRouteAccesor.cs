using Jds.Sdr.Server.Controllers;

namespace Jds.Sdr.Server.Attributes
{
	public static class EntityControllerRouteAccesor
	{

		public static string GetEntityName(Type controllerType)
		{
			EntityRouteAttribute routeAttribute = controllerType.GetCustomAttributes(true)
				.Where(x => x.GetType() == typeof(EntityRouteAttribute))
				.FirstOrDefault() as EntityRouteAttribute;
			if (routeAttribute is null)
				throw new Exception($"{controllerType.Name} does not have a related RouteAttribute.");
			return routeAttribute.EntityType.Name;
		}

		public static string GetEndPointRoute(Type controllerType)
		{
			EntityRouteAttribute routeAttribute = controllerType.GetCustomAttributes(true)
				.Where(x => x.GetType() == typeof(EntityRouteAttribute))
				.FirstOrDefault() as EntityRouteAttribute;
			if (routeAttribute is null)
				throw new Exception($"{controllerType.Name} does not have a related RouteAttribute.");
			return ApiController.Template.Replace(ApiController.PlaceholderName, routeAttribute.Name);
		}

	}
}
