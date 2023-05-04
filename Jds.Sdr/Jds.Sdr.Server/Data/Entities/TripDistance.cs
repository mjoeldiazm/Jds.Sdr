using Jds.Sdr.Shared.Interfaces;

namespace Jds.Sdr.Server.Data.Entities
{
	public class TripDistance : IDataEntity<int>
	{
		public int Id { get; set; }

		public int TripId { get; set; }

		public Trip Trip { get; set; }

		public int DistanceId { get; set; }

		public Distance Distance { get; set; }
	}
}
