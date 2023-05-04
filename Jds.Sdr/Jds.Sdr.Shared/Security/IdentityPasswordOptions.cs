using Microsoft.AspNetCore.Identity;

namespace Jds.Sdr.Shared.Security
{
	public static class IdentityPasswordOptions
	{
		public static PasswordOptions DefaultOptions { get; } = new PasswordOptions()
		{
			RequireDigit = true,
			RequiredLength = 10,
			RequireLowercase = true,
			RequireNonAlphanumeric = true,
			RequireUppercase = true,
		};
	}
}
