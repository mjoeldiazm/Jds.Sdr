using Jds.Sdr.Server.Data;
using Jds.Sdr.Server.Data.Entities;
using Jds.Sdr.Shared.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace Jds.Sdr.Server
{
	public static class Dependencies
	{
		public static void AddAutoMapperProfiles(this IServiceCollection services)
		{
			services.AddAutoMapper(Assembly.GetExecutingAssembly());
		}

		public static void AddApplicationDbContext(this IServiceCollection services)
		{
			services.AddDbContext<ApplicationDbContext>(ServiceLifetime.Transient);
		}

		public static void AddIdentity(this IServiceCollection services, WebApplicationBuilder builder)
		{
			services.AddIdentity<UserAccount, Role>(options =>
				{
					options.Password.RequireDigit = IdentityPasswordOptions.DefaultOptions.RequireDigit;
					options.Password.RequiredLength = IdentityPasswordOptions.DefaultOptions.RequiredLength;
					options.Password.RequireLowercase =
						IdentityPasswordOptions.DefaultOptions.RequireLowercase;
					options.Password.RequireUppercase =
						IdentityPasswordOptions.DefaultOptions.RequireUppercase;
					options.Password.RequireNonAlphanumeric =
						IdentityPasswordOptions.DefaultOptions.RequireNonAlphanumeric;
					options.SignIn.RequireConfirmedEmail = false;
				})
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddDefaultTokenProviders();
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = false,
					ValidateAudience = false,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(
						Encoding.UTF8.GetBytes(builder.Configuration["JwtKey"])),
					ClockSkew = TimeSpan.Zero
				}
			);
			services.AddScoped<UserManager<UserAccount>>();
		}
	}
}
