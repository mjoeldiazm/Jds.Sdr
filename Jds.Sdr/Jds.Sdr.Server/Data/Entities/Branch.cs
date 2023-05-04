using Jds.Sdr.Shared.Interfaces;

namespace Jds.Sdr.Server.Data.Entities
{
	public class Branch : IDataEntity<int>
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Location { get; set; }

		public ICollection<Distance> Distances { get; set; }

		public ICollection<Trip> Trips { get; set; }
	}
}
