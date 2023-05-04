namespace Jds.Sdr.Shared.Security
{
	public static class RoleDefinitions
	{
		public const string All = $"{Administrator},{StoreManager},{Seller}";
		public const string Administrator = "Administrador";
		public const string AdministratorAndStoreManager = $"{Administrator},{StoreManager}";
		public const string Seller = "Vendedor";
		public const string StoreManager = "Gerente de Tienda";
	}
}
