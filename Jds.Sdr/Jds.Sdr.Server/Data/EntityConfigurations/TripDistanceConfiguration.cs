using Jds.Sdr.Server.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jds.Sdr.Server.Data.EntityConfigurations
{
	public class TripDistanceConfiguration : IEntityTypeConfiguration<TripDistance>
	{
		public void Configure(EntityTypeBuilder<TripDistance> builder)
		{
			builder.ToTable("TripDistances");

			builder.HasKey(x => x.Id);

			builder.Property(x => x.Id)
				.HasColumnOrder(0);

			builder.Property(x => x.TripId)
				.HasColumnOrder(1);

			builder.Property(x => x.DistanceId)
				.HasColumnOrder(2);

			builder.HasOne(x => x.Trip)
				.WithMany(x => x.TripDistances)
				.HasForeignKey(x => x.TripId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(x => x.Distance)
				.WithMany(x => x.TripDistances)
				.HasForeignKey(x => x.DistanceId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
