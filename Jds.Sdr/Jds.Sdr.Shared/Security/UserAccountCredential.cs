using Jds.Sdr.Shared.Data.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace Jds.Sdr.Shared.Security
{
	public class UserAccountCredential
	{
		[Display(Name = AttributeNames.EmailAddress)]
		[Required(ErrorMessage = AttributeErrorMessages.FieldRequired)]
		[EmailAddress(ErrorMessage = AttributeErrorMessages.InvalidFormat)]
		public string EmailAddress { get; set; }

		[Display(Name = AttributeNames.Password)]
		[Required(ErrorMessage = AttributeErrorMessages.FieldRequired)]
		public string Password { get; set; }
	}
}
