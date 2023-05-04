using System.Text.Json;
using System.Text.Json.Serialization;

namespace Jds.Sdr.Shared.Interfaces
{
	public abstract class ClonableEquatable<T> : IClonable<T>, IEquatable<T>
	{
		public T Clone()
		{
			JsonSerializerOptions options = new() { ReferenceHandler = ReferenceHandler.Preserve };
			var serialized = JsonSerializer.Serialize(this, GetType(), options);
			return JsonSerializer.Deserialize<T>(serialized, options);
		}

		public bool Equals(T other)
		{
			JsonSerializerOptions options = new() { ReferenceHandler = ReferenceHandler.Preserve };
			if (other == null)
			{
				return false;
			}
			return JsonSerializer.Serialize(this, options) == JsonSerializer.Serialize(other, GetType(), options);
		}
	}
}
