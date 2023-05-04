using Jds.Sdr.Server.Data.Entities;
using Jds.Sdr.Server.Data.Sql;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jds.Sdr.Server.Data.EntityConfigurations
{
	public class UserAccountConfiguration : IEntityTypeConfiguration<UserAccount>
	{
		public void Configure(EntityTypeBuilder<UserAccount> builder)
		{
			builder.ToTable("UserAccounts");

			builder.HasKey(x => x.Id);

			builder.Property(x => x.Id)
				.HasColumnOrder(0);

			builder.Property(x => x.CreationDateTime)
				.HasColumnOrder(2)
				.IsRequired()
				.HasColumnType(SqlDataTypes.DateTime)
				.HasDefaultValueSql(SqlDefaultValues.CurrentDateTime);

			builder.HasOne(x => x.Employee)
				.WithOne(x => x.UserAccount)
				.HasForeignKey<UserAccount>(x => x.EmployeeId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
