using Jds.Sdr.Server.Data.Entities;
using Jds.Sdr.Server.Data.Sql;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jds.Sdr.Server.Data.EntityConfigurations
{
	public class TripConfiguration : IEntityTypeConfiguration<Trip>
	{
		public void Configure(EntityTypeBuilder<Trip> builder)
		{
			builder.ToTable("Trips");

			builder.HasKey(x => x.Id);

			builder.Property(x => x.Id)
				.HasColumnOrder(0);

			builder.Property(x => x.CreatorId)
				.HasColumnOrder(1);

			builder.Property(x => x.CarrierId)
				.HasColumnOrder(2);

			builder.Property(x => x.BranchId)
				.HasColumnOrder(3);

			builder.Property(x => x.Date)
				.HasColumnOrder(4)
				.IsRequired()
				.HasColumnType(SqlDataTypes.Date);

			builder.HasOne(x => x.Carrier)
				.WithMany(x => x.Trips)
				.HasForeignKey(x => x.CarrierId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(x => x.Creator)
				.WithMany(x => x.CreatedTrips)
				.HasForeignKey(x => x.CreatorId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(x => x.Branch)
				.WithMany(x => x.Trips)
				.HasForeignKey(x => x.BranchId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
