using Jds.Sdr.Server.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Jds.Sdr.Server.Data
{
	public class ApplicationDbContext : IdentityDbContext<UserAccount, Role, int>
	{
		private readonly IConfiguration configuration;

		public DbSet<Branch> Branches { get; set; }
		public DbSet<Carrier> Carriers { get; set; }
		public DbSet<Distance> Distances { get; set; }
		public DbSet<Employee> Employees { get; set; }
		public DbSet<TripDistance> TripDistances { get; set; }
		public DbSet<Trip> Trips { get; set; }

		public ApplicationDbContext(IConfiguration configuration)
		{
			this.configuration = configuration;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}
	}
}
