using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jds.Sdr.Server.Data.EntityConfigurations
{
	public class UserAccountLoginConfiguration : IEntityTypeConfiguration<IdentityUserLogin<int>>
	{
		public void Configure(EntityTypeBuilder<IdentityUserLogin<int>> builder)
		{
			builder.ToTable("UserAccountLogins");
		}
	}
}
