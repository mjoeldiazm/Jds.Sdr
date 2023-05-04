using Jds.Sdr.Shared.Data.DataAnnotations;
using Jds.Sdr.Shared.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Jds.Sdr.Shared.Data.EntityViews
{
	public class RoleView : ClonableEquatable<RoleView>, IDataEntity<int>
	{
		[Key]
		public int Id { get; set; }

		[Display(Name = AttributeNames.Name)]
		[Required(ErrorMessage = AttributeErrorMessages.FieldRequired)]
		[StringLength(256, ErrorMessage = AttributeErrorMessages.StringMaximumLength)]
		public string Name { get; set; }

		[Display(Name = AttributeNames.Description)]
		[Required(ErrorMessage = AttributeErrorMessages.FieldRequired)]
		[StringLength(int.MaxValue, ErrorMessage = AttributeErrorMessages.StringMaximumLength)]
		public string Description { get; set; }

		[Display(Name = AttributeNames.NormalizedName)]
		[Required(ErrorMessage = AttributeErrorMessages.FieldRequired)]
		[StringLength(int.MaxValue, ErrorMessage = AttributeErrorMessages.StringMaximumLength)]
		public string NormalizedName { get; set; }

		public ICollection<UserAccountView> UserAccounts { get; set; }
	}
}
