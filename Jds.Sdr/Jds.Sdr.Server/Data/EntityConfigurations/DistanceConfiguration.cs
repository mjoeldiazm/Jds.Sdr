using Jds.Sdr.Server.Data.Entities;
using Jds.Sdr.Server.Data.Sql;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jds.Sdr.Server.Data.EntityConfigurations
{
	public class DistanceConfiguration : IEntityTypeConfiguration<Distance>
	{
		public void Configure(EntityTypeBuilder<Distance> builder)
		{
			builder.ToTable("Distances");

			builder.HasKey(x => x.Id);

			builder.Property(x => x.Id)
				.HasColumnOrder(0);

			builder.HasIndex(x => new { x.BranchId, x.EmployeeId })
				.IsUnique();

			builder.Property(x => x.BranchId)
				.HasColumnOrder(1);

			builder.Property(x => x.EmployeeId)
				.HasColumnOrder(2);

			builder.Property(x => x.DistanceInKilometers)
				.HasColumnOrder(3)
				.IsRequired()
				.HasColumnType(SqlDataTypes.Float);

			builder.HasOne(x => x.Branch)
				.WithMany(x => x.Distances)
				.HasForeignKey(x => x.BranchId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(x => x.Employee)
				.WithMany(x => x.Distances)
				.HasForeignKey(x => x.EmployeeId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
