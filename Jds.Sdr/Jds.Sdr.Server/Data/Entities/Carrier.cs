using Jds.Sdr.Shared.Interfaces;

namespace Jds.Sdr.Server.Data.Entities
{
	public class Carrier : IDataEntity<int>
	{
		public int Id { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string FullName
		{
			get => $"{FirstName} {LastName}";
		}

		public string PhoneNumber { get; set; }

		public string EmailAddress { get; set; }

		public string IdentityNationalDocument { get; set; }

		public double RatePerKilometer { get; set; }

		public ICollection<Trip> Trips { get; set; }
	}
}
