using Jds.Sdr.Shared.Data.EntityViews;
using Jds.Sdr.Shared.Interfaces;
using Jds.Sdr.Shared.Security;
using Jds.Sdr.Shared.Services;
using Jds.Sdr.WebClient.Extensions;
using Jds.Sdr.WebClient.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace Jds.Sdr.WebClient.Security
{
	public class JwtAuthenticationProvider : AuthenticationStateProvider, ILoginService
	{
		public const string TokenKey = "TokenKey";
		public const string ExpirationTokenKey = "ExpirationTokenKey";
		public const int TokenRenewalThresholdInMinutes = 5;

		private readonly IJSRuntime jSRuntime;
		private readonly HttpClient httpClient;
		private readonly IUserAccountLogin userAccountLoginService;

		private static AuthenticationState AnonymousUser => new(new ClaimsPrincipal(new ClaimsIdentity()));

		public JwtAuthenticationProvider(IJSRuntime jS, HttpClient httpClient,
			IUserAccountLogin userAccountLoginService)
		{
			this.jSRuntime = jS;
			this.httpClient = httpClient;
			this.userAccountLoginService = userAccountLoginService;
		}

		public async override Task<AuthenticationState> GetAuthenticationStateAsync()
		{
			object token = await jSRuntime.GetFromLocalStorageAsync(TokenKey);
			if (token is null)
				return AnonymousUser;
			var expirationDateTimeObject = await jSRuntime.GetFromLocalStorageAsync(ExpirationTokenKey);
			if (expirationDateTimeObject is null)
			{
				await CleanTokenInLocalStorage();
				return AnonymousUser;
			}
			if (DateTime.TryParse(expirationDateTimeObject.ToString(), out DateTime tokenExpirationDateTime))
			{
				if (IsTokenExpired(tokenExpirationDateTime))
				{
					await CleanTokenInLocalStorage();
					return AnonymousUser;
				}
				if (IsNecessaryRenewToken(tokenExpirationDateTime))
					token = await RenewToken(token.ToString());
			}
			return BuildAuthenticationState(token.ToString());
		}

		private AuthenticationState BuildAuthenticationState(string token)
		{
			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
			IEnumerable<Claim> claims = ParseClaimsFromJwt(token);
			return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt")));
		}

		private static IEnumerable<Claim> ParseClaimsFromJwt(string token)
		{
			JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
			JwtSecurityToken desearialedToken = jwtSecurityTokenHandler.ReadJwtToken(token);
			return desearialedToken.Claims;
		}

		public async Task Login(UserAccountLoginInformation loginInformation)
		{
			await jSRuntime.SaveInLocalStorageAsync(TokenKey, loginInformation.Token.Value);
			await jSRuntime.SaveInLocalStorageAsync(ExpirationTokenKey, loginInformation.Token.ExpirationDateTime.ToString());
			AuthenticationState authenticationState = BuildAuthenticationState(loginInformation.Token.Value);
			NotifyAuthenticationStateChanged(Task.FromResult(authenticationState));
		}

		public async Task Logout()
		{
			await CleanTokenInLocalStorage();
			NotifyAuthenticationStateChanged(Task.FromResult(AnonymousUser));
		}

		private async Task<string> RenewToken(string token)
		{
			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
			DataServiceResponse<UserAccountTokenView> serviceResponse = await userAccountLoginService.RenewToken();
			await jSRuntime.SaveInLocalStorageAsync(TokenKey, serviceResponse.Response.Value);
			await jSRuntime.SaveInLocalStorageAsync(ExpirationTokenKey, serviceResponse.Response.ExpirationDateTime.ToString());
			return serviceResponse.Response.Value;
		}

		public async Task RenewTokenHandler()
		{
			var expirationDateTimeObject = await jSRuntime.GetFromLocalStorageAsync(ExpirationTokenKey);
			if (DateTime.TryParse(expirationDateTimeObject.ToString(), out DateTime tokenExpirationDateTime))
			{
				if (IsTokenExpired(tokenExpirationDateTime))
				{
					await Logout();
				}
				if (IsNecessaryRenewToken(tokenExpirationDateTime))
				{
					object token = await jSRuntime.GetFromLocalStorageAsync(TokenKey);
					string newToken = await RenewToken(token.ToString());
					AuthenticationState authenticationState = BuildAuthenticationState(newToken);
					NotifyAuthenticationStateChanged(Task.FromResult(authenticationState));
				}
			}
		}

		private async Task CleanTokenInLocalStorage()
		{
			await jSRuntime.RemoveFromLocalStorageAsync(TokenKey);
			await jSRuntime.RemoveFromLocalStorageAsync(ExpirationTokenKey);
			httpClient.DefaultRequestHeaders.Authorization = null;
		}

		private static bool IsTokenExpired(DateTime expirationDateTime)
		{
			return expirationDateTime <= DateTime.UtcNow;
		}

		private static bool IsNecessaryRenewToken(DateTime expirationDateTime)
		{
			return expirationDateTime.Subtract(DateTime.UtcNow) < TimeSpan.FromMinutes(TokenRenewalThresholdInMinutes);
		}
	}
}
