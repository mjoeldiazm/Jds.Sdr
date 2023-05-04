using Jds.Sdr.Server.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jds.Sdr.Server.Data.EntityConfigurations
{
	public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
	{
		public void Configure(EntityTypeBuilder<Employee> builder)
		{
			builder.ToTable("Employees");

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
		}
	}
}
