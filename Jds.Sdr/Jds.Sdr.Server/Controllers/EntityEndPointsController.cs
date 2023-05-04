using Jds.Sdr.Server.Attributes;
using Jds.Sdr.Shared.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jds.Sdr.Server.Controllers
{
	[ApiController]
	[Route(ApiController.Template)]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public class EntityEndPointsController : ControllerBase
	{
		private static List<EntityControllerInformation> entityRoutes;

		[HttpGet]
		[AllowAnonymous]
		public ActionResult<List<EntityControllerInformation>> GetEntityRoutes()
		{
			if (entityRoutes is null)
			{
				entityRoutes = new();
				List<Type> entityControllers = new();
				entityControllers = AppDomain.CurrentDomain.GetAssemblies()
					.SelectMany(assembly => assembly.GetTypes())
					.Where(x => x.IsClass
						&& !x.IsAbstract
						&& x.BaseType is not null
						&& x.BaseType.IsGenericType
						&& x.BaseType.GetGenericTypeDefinition() == typeof(EntityControllerBase<,>))
					.ToList();
				entityRoutes = entityControllers.Select(x => new EntityControllerInformation
				{
					EntityName = EntityControllerRouteAccesor.GetEntityName(x),
					EndPointRoute = EntityControllerRouteAccesor.GetEndPointRoute(x)
				}).ToList();
			}
			return entityRoutes;
		}
	}
}
