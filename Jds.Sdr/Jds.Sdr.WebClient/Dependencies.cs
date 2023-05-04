using Jds.Sdr.Shared.Data.EntityViews;
using Jds.Sdr.Shared.Interfaces;
using Jds.Sdr.WebClient.Interfaces;
using Jds.Sdr.WebClient.Security;
using Jds.Sdr.WebClient.Services;
using Jds.Sdr.WebClient.Services.Entities;
using Microsoft.AspNetCore.Components.Authorization;

namespace Jds.Sdr.WebClient
{
	internal static class Dependencies
	{
		public static void AddDataRepositories(this IServiceCollection services)
		{
			services.AddScoped<IDataRepository<BranchView, int>, BranchDataRepository>();
			services.AddScoped<IDataRepository<CarrierView, int>, CarrierDataRepository>();
			services.AddScoped<IDataRepository<DistanceView, int>, DistanceDataRepository>();
			services.AddScoped<IDataRepository<EmployeeView, int>, EmployeeDataRepository>();
			services.AddScoped<IDataRepository<RoleView, int>, RoleDataRepository>();
			services.AddScoped<IDataRepository<TripView, int>, TripDataRepository>();
			services.AddScoped<IDataRepository<TripDistanceView, int>, TripDistanceDataRepository>();
			services.AddScoped<IDataRepository<UserAccountView, int>, UserAccountDataRepository>();
		}

		public static void AddEntityControllerProvider(this IServiceCollection services)
		{
			services.AddScoped<IEntityControllerProvider, EntityControllerProvider>();
		}

		public static void AddAuthorization(this IServiceCollection services)
		{
			services.AddAuthenticationCore();
			services.AddApiAuthorization();
			services.AddScoped<JwtAuthenticationProvider>();
			services.AddScoped<AuthenticationStateProvider, JwtAuthenticationProvider>(provider =>
				provider.GetRequiredService<JwtAuthenticationProvider>());
			services.AddScoped<ILoginService, JwtAuthenticationProvider>(provider =>
				provider.GetRequiredService<JwtAuthenticationProvider>());
			services.AddScoped<IUserAccountLogin, UserAccountDataRepository>();
			services.AddScoped<UserAccountTokenRenewer>();
		}
	}
}
