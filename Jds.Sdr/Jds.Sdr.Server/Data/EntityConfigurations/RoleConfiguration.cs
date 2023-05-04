using Jds.Sdr.Server.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jds.Sdr.Server.Data.EntityConfigurations
{
	public class RoleConfiguration : IEntityTypeConfiguration<Role>
	{
		public void Configure(EntityTypeBuilder<Role> builder)
		{
			builder.ToTable("Roles");

			builder.Property(x => x.Id)
				.HasColumnOrder(0);

			builder.Property(x => x.Name)
				.HasColumnOrder(1);

			builder.Property(x => x.NormalizedName)
				.HasColumnOrder(2);

			builder.Property(x => x.Description)
				.HasColumnOrder(3);

			builder.Property(x => x.ConcurrencyStamp)
				.HasColumnOrder(4);
		}
	}
}
