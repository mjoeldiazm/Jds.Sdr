using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jds.Sdr.Server.Data.EntityConfigurations
{
	public class UserAccountRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<int>>
	{
		public void Configure(EntityTypeBuilder<IdentityUserRole<int>> builder)
		{
			builder.ToTable("UserAccountRoles");
		}
	}
}
