using Jds.Sdr.Shared.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Jds.Sdr.Server.Data.Entities
{
	public class UserAccount : IdentityUser<int>, IDataEntity<int>
	{
		public int EmployeeId { get; set; }

		public DateTime CreationDateTime { get; set; }

		public Employee Employee { get; set; }

	}
}
