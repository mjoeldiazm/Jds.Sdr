using Jds.Sdr.Shared.Interfaces;

namespace Jds.Sdr.Server.Data.Entities
{
	public class Trip : IDataEntity<int>
	{
		public int Id { get; set; }

		public int CarrierId { get; set; }

		public Carrier Carrier { get; set; }

		public int BranchId { get; set; }

		public Branch Branch { get; set; }

		public int CreatorId { get; set; }

		public Employee Creator { get; set; }

		public DateTime Date { get; set; }

		public ICollection<TripDistance> TripDistances { get; set; }
	}
}
