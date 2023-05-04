using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jds.Sdr.Server.Data.EntityConfigurations
{
	public class UserAccountTokenConfiguration : IEntityTypeConfiguration<IdentityUserToken<int>>
	{
		public void Configure(EntityTypeBuilder<IdentityUserToken<int>> builder)
		{
			builder.ToTable("UserAccountTokens");
		}
	}
}
