using Jds.Sdr.Server.Data.Entities;
using Jds.Sdr.Server.Data.Sql;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jds.Sdr.Server.Data.EntityConfigurations
{
	public class BranchConfiguration : IEntityTypeConfiguration<Branch>
	{
		public void Configure(EntityTypeBuilder<Branch> builder)
		{
			builder.ToTable("Branches");

			builder.HasKey(x => x.Id);

			builder.Property(x => x.Id)
				.HasColumnOrder(0);

			builder.Property(x => x.Name)
				.HasColumnOrder(1)
				.IsRequired()
				.HasMaxLength(256);

			builder.Property(x => x.Location)
				.HasColumnOrder(2)
				.HasColumnType(SqlDataTypes.VarCharMax);
		}
	}
}
