using System.Reflection;

namespace Jds.Sdr.Shared.Extensions
{
	public static class AttributeExtensionMethods
	{
		public static TAttribute GetAttribute<TAttribute>(this Enum enumValue)
		where TAttribute : Attribute
		{
			return enumValue.GetType()
				.GetMember(enumValue.ToString())
				.First()
				.GetCustomAttribute<TAttribute>();
		}
	}
}
