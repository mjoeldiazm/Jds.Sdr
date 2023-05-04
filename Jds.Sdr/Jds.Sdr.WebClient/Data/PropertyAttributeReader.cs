using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Jds.Sdr.WebClient.Data
{
	public class PropertyAttributeReader<T>
	{

		private static DisplayAttribute GetDisplayAttribute(string propertyName)
		{
			DisplayAttribute attribute = null;
			if (!propertyName.Contains('.'))
			{
				attribute = typeof(T)
				.GetProperty(propertyName)
				.GetCustomAttributes(false)
				.OfType<DisplayAttribute>()
				.FirstOrDefault();
			}
			else
			{
				Type readerType = typeof(PropertyAttributeReader<>);
				var properties = propertyName.Split('.').ToList();
				var propertyType = typeof(T)
					.GetProperty(properties.FirstOrDefault())
					.PropertyType;
				Type[] typeArgs = { propertyType };
				Type readerConstructor = readerType.MakeGenericType(typeArgs);
				var readerInstance = Activator.CreateInstance(readerConstructor);
				var getDisplayAttributeMethodInfo = readerConstructor.GetMethod
					(
						nameof(PropertyAttributeReader<T>.GetDisplayAttribute),
						BindingFlags.Static | BindingFlags.NonPublic
					);
				if (properties.Count > 1)
				{
					properties.RemoveAt(0);
				}
				attribute = getDisplayAttributeMethodInfo.Invoke
					(
						readerInstance,
						new object[] { string.Join('.', properties) }
					) as DisplayAttribute;
			}
			return attribute;
		}

		public string GetName(string propertyName)
		{
			var attribute = GetDisplayAttribute(propertyName);
			if (attribute != null)
			{
				attribute.Name = attribute.Name.Replace('.', '_');
			}
			return attribute != null ? attribute.GetName() : string.Empty;
		}

	}
}
