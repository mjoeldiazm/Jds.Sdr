using AutoMapper;
using DevExpress.Blazor.Internal;
using Jds.Sdr.Server.Attributes;
using Jds.Sdr.Server.Data;
using Jds.Sdr.Server.Data.Entities;
using Jds.Sdr.Shared.Data.EntityViews;
using Jds.Sdr.Shared.Interfaces;
using Jds.Sdr.Shared.Security;
using Jds.Sdr.Shared.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Jds.Sdr.Server.Controllers.Authentication
{
	[EntityRoute(ApiController.Template, typeof(UserAccountView), nameof(UserAccountsController))]
	public class UserAccountsController : EntityControllerBase<UserAccount, UserAccountView>,
		IUserAccountLogin
	{
		private readonly IMapper mapper;
		private readonly UserManager<UserAccount> userManager;
		private readonly RoleManager<Role> roleManager;
		private readonly SignInManager<UserAccount> signInManager;
		private readonly IConfiguration configuration;

		public UserAccountsController(ApplicationDbContext dbContext, IMapper mapper,
			UserManager<UserAccount> userManager,
			RoleManager<Role> roleManager,
			SignInManager<UserAccount> signInManager,
			IConfiguration configuration)
			: base(dbContext, mapper)
		{
			this.mapper = mapper;
			this.userManager = userManager;
			this.roleManager = roleManager;
			this.signInManager = signInManager;
			this.configuration = configuration;
		}

		[HttpGet]
		public async override Task<DataServiceResponse<IEnumerable<UserAccountView>>> GetAllAsync()
		{
			DataServiceResponse<IEnumerable<UserAccountView>> userAccountServiceResponse = await base.GetAllAsync();
			IEnumerable<RoleView> roles = mapper.Map<IEnumerable<RoleView>>(await DbContext.Roles.ToListAsync());
			foreach (UserAccountView item in userAccountServiceResponse.Response)
			{
				int? userRoleId = await DbContext.UserRoles
					.Where(x => x.UserId.Equals(item.Id))
					.Select(x => x.RoleId)
					.FirstOrDefaultAsync();
				if (userRoleId is null)
					break;
				Role role = await DbContext.Roles
					.Where(x => x.Id.Equals(userRoleId))
					.FirstOrDefaultAsync();
				if (role is null)
					break;
				item.RoleId = role.Id;
				item.Role = role.Name;
			}
			return userAccountServiceResponse;
		}

		[HttpPost]
		public async override Task<DataServiceResponse<int>> PostAsync(UserAccountView objectToPost)
		{
			UserAccount userAccount = mapper.Map<UserAccount>(objectToPost);
			string password = "Control2023*";
			IdentityResult userCreateResult = await userManager.CreateAsync(userAccount, password);
			if (userCreateResult.Succeeded)
			{
				await userManager.AddToRoleAsync(userAccount, objectToPost.Role);
				UserAccountCredential userAccountCredential = new()
				{
					EmailAddress = userAccount.Email,
					Password = password,
				};
				return new DataServiceResponse<int>(1, userAccount.Id, DataServiceResponseMessages.ItemAdded);
			}
			else
				return new DataServiceResponse<int>(true, userCreateResult.Errors.First().Description);
		}

		[HttpPut]
		public async override Task<DataServiceResponse<int>> PutAsync(UserAccountView objectToPut)
		{
			UserAccount userAccount = await userManager.FindByEmailAsync(objectToPut.EmailAddress);
			if (userAccount is not null)
			{
				string changeUserRoleResult = await ChangeUserRole(userAccount, objectToPut.Role);
				List<string> operationResults = new();
				if (!changeUserRoleResult.Equals(string.Empty))
					operationResults.Add(changeUserRoleResult);
				if (operationResults.Count > 0)
					return new DataServiceResponse<int>
						(1, operationResults.Count, string.Join(Environment.NewLine, operationResults.ToArray()));
				else
					return new DataServiceResponse<int>(false, string.Empty);
			}
			return new DataServiceResponse<int>(true, DataServiceResponseMessages.ItemNotFound);
		}

		[HttpDelete("{id:int}")]
		public async override Task<DataServiceResponse<int>> DeleteAsync(int id)
		{
			UserAccount userAccount = await userManager.FindByIdAsync(id.ToString());
			if (userAccount is not null)
			{
				IdentityResult userDeleteResult = await userManager.DeleteAsync(userAccount);
				if (userDeleteResult.Succeeded)
					return new DataServiceResponse<int>(true, DataServiceResponseMessages.ItemDeleted);
				else
					return new DataServiceResponse<int>(true, DataServiceResponseMessages.ItemNotDeleted);
			}
			return new DataServiceResponse<int>(true, DataServiceResponseMessages.ItemNotFound);
		}

		[HttpPost("Login")]
		[AllowAnonymous]
		public async Task<DataServiceResponse<UserAccountLoginInformation>> Login(UserAccountCredential userAccountCredential)
		{
			UserAccount currentUserAccount = await DbContext.Users
				.Where(x => x.UserName.Equals(userAccountCredential.EmailAddress))
				.FirstOrDefaultAsync();
			if (currentUserAccount is null)
				return new DataServiceResponse<UserAccountLoginInformation>(true, "Usuario no registrado.");
			var loginResult = await signInManager.PasswordSignInAsync(userAccountCredential.EmailAddress,
				 userAccountCredential.Password, isPersistent: false, lockoutOnFailure: false);
			if (loginResult.Succeeded)
			{
				UserAccountTokenView userAccountToken = await BuildUserAccountToken(currentUserAccount);
				UserAccountLoginInformation loginInformation = new()
				{
					Token = userAccountToken
				};
				return new DataServiceResponse<UserAccountLoginInformation>(loginInformation);
			}
			else
				return new DataServiceResponse<UserAccountLoginInformation>(true, "Contraseña incorrecta.");
		}

		[HttpGet("RenewToken")]
		public async Task<DataServiceResponse<UserAccountTokenView>> RenewToken()
		{
			UserAccount currentUserAccount = await userManager.FindByEmailAsync(HttpContext.User.Identity.Name);
			UserAccountTokenView userAccountToken = await BuildUserAccountToken(currentUserAccount);
			return new DataServiceResponse<UserAccountTokenView>(userAccountToken);
		}

		private async Task<UserAccountTokenView> BuildUserAccountToken(UserAccount userAccount)
		{
			List<Claim> claims = new()
			{
				new Claim(ClaimTypes.Name, userAccount.Email)
			};
			List<string> roles = (await userManager.GetRolesAsync(userAccount)).ToList();
			roles.ForEach(x => claims.Add(new Claim(ClaimTypes.Role, x)));
			SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(configuration["JwtKey"]));
			SigningCredentials signingCredentials = new(key, SecurityAlgorithms.HmacSha256);
			DateTime expirationDateTime = DateTime.UtcNow
				.AddHours(Convert.ToDouble(configuration["DefaultTokenDurationInHours"]));
			JwtSecurityToken token = new(
				issuer: null,
				audience: null,
				claims: claims,
				expires: expirationDateTime,
				signingCredentials: signingCredentials);
			return new UserAccountTokenView
			{
				Value = new JwtSecurityTokenHandler().WriteToken(token),
				ExpirationDateTime = expirationDateTime
			};
		}

		private async Task<string> ChangeUserRole(UserAccount userAccount, string role)
		{
			string currentRole = (await userManager.GetRolesAsync(userAccount))
				.FirstOrDefault();
			if (currentRole.IsNullOrEmpty())
				return string.Empty;
			if (currentRole.Equals(role))
				return string.Empty;
			if (await roleManager.RoleExistsAsync(role))
			{
				await userManager.RemoveFromRoleAsync(userAccount, currentRole);
				await userManager.AddToRoleAsync(userAccount, role);
				return "Cambio de rol aplicado.";
			}
			return "Rol no encontrado.";
		}

		protected override IQueryable<UserAccount> GetAllAsQueryable()
		{
			return DbContext.Users
				.Include(x => x.Employee)
				.OrderBy(x => x.Employee.FirstName)
				.ThenBy(x => x.Employee.LastName)
				.AsQueryable();
		}

	}
}
