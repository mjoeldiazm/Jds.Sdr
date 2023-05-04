using Microsoft.AspNetCore.Mvc;

namespace Jds.Sdr.Server.Attributes
{
	public class EntityRouteAttribute : RouteAttribute
	{
		private Type entityType;

		public Type EntityType
		{
			get => entityType;
			set => entityType = value;
		}

		public EntityRouteAttribute(string template, Type entityType, string name)
			: base(template)
		{
			this.entityType = entityType;
			Name = name.EndsWith("Controller") ? name.Replace("Controller", string.Empty) : name;
		}
	}
}
