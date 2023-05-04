namespace Jds.Sdr.WebClient.Components
{
	public class ComponentParameter<TValue>
	{
		public Action<TValue> Action { get; set; }
		public Action VoidAction { get; set; }
		public Action<Type> GoTo { get; set; }
		public TValue Value { get; set; }
	}
}
