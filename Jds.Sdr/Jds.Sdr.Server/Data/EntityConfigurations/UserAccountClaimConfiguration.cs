using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jds.Sdr.Server.Data.EntityConfigurations
{
	public class UserAccountClaimConfiguration : IEntityTypeConfiguration<IdentityUserClaim<int>>
	{
		public void Configure(EntityTypeBuilder<IdentityUserClaim<int>> builder)
		{
			builder.ToTable("UserAccountClaims");
		}
	}
}
