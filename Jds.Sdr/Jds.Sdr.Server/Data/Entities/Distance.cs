using Jds.Sdr.Shared.Interfaces;

namespace Jds.Sdr.Server.Data.Entities
{
	public class Distance : IDataEntity<int>
	{
		public int Id { get; set; }

		public int BranchId { get; set; }

		public int EmployeeId { get; set; }

		public double DistanceInKilometers { get; set; }

		public Branch Branch { get; set; }

		public Employee Employee { get; set; }

		public ICollection<TripDistance> TripDistances { get; set; }
	}
}
