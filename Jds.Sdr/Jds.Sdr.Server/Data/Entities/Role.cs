using Jds.Sdr.Shared.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Jds.Sdr.Server.Data.Entities
{
	public class Role : IdentityRole<int>, IDataEntity<int>
	{
		public string Description { get; set; }
	}
}