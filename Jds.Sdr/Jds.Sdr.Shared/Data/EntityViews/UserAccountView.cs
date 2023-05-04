using Jds.Sdr.Shared.Data.DataAnnotations;
using Jds.Sdr.Shared.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Jds.Sdr.Shared.Data.EntityViews
{
	public class UserAccountView : ClonableEquatable<UserAccountView>, IDataEntity<int>
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = AttributeErrorMessages.FieldRequired)]
		public int? EmployeeId { get; set; }

		[Required(ErrorMessage = AttributeErrorMessages.FieldRequired)]
		public int RoleId { get; set; }

		[Display(Name = AttributeNames.EmailAddress)]
		[Required(ErrorMessage = AttributeErrorMessages.FieldRequired)]
		[StringLength(32, ErrorMessage = AttributeErrorMessages.StringMaximumLength)]
		public string EmailAddress { get; set; }

		[Display(Name = AttributeNames.PhoneNumber)]
		[Required(ErrorMessage = AttributeErrorMessages.FieldRequired)]
		[StringLength(16, ErrorMessage = AttributeErrorMessages.StringMaximumLength)]
		public string PhoneNumber { get; set; }

		[Display(Name = AttributeNames.CreationDateTime)]
		[Required(ErrorMessage = AttributeErrorMessages.FieldRequired)]
		public DateTime CreationDateTime { get; set; }

		[Display(Name = AttributeNames.Employee)]
		public EmployeeView Employee { get; set; }

		[Display(Name = AttributeNames.Role)]
		[Required(ErrorMessage = AttributeErrorMessages.FieldRequired)]
		[StringLength(int.MaxValue, ErrorMessage = AttributeErrorMessages.StringMaximumLength)]
		public string Role { get; set; }
	}
}
