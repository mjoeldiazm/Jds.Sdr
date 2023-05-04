using Jds.Sdr.Server.Data.Entities;
using Jds.Sdr.Server.Data.Sql;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jds.Sdr.Server.Data.EntityConfigurations
{
	public class CarrierConfiguration : IEntityTypeConfiguration<Carrier>
	{
		public void Configure(EntityTypeBuilder<Carrier> builder)
		{
			builder.ToTable("Carriers");

			builder.HasKey(x => x.Id);

			builder.HasIndex(x => x.EmailAddress)
				.IsUnique();

			builder.Property(x => x.Id)
				.HasColumnOrder(0);

			builder.Property(x => x.FirstName)
				.HasColumnOrder(1)
				.IsRequired()
				.HasMaxLength(64);

			builder.Property(x => x.LastName)
				.HasColumnOrder(2)
				.IsRequired()
				.HasMaxLength(64);

			builder.Property(x => x.PhoneNumber)
				.HasColumnOrder(3)
				.HasMaxLength(16);

			builder.Property(x => x.EmailAddress)
				.HasColumnOrder(4)
				.IsRequired()
				.HasMaxLength(64);

			builder.Property(x => x.IdentityNationalDocument)
				.HasColumnOrder(5)
				.IsRequired()
				.HasMaxLength(16);

			builder.Property(x => x.RatePerKilometer)
				.HasColumnOrder(6)
				.IsRequired()
				.HasColumnType(SqlDataTypes.Float);
		}
	}
}
