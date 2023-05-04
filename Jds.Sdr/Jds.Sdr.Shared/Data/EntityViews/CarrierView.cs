using Jds.Sdr.Shared.Data.DataAnnotations;
using Jds.Sdr.Shared.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Jds.Sdr.Shared.Data.EntityViews
{
	public class CarrierView : ClonableEquatable<CarrierView>, IDataEntity<int>
	{
		[Key]
		public int Id { get; set; }

		[Display(Name = AttributeNames.FirstName)]
		[Required(ErrorMessage = AttributeErrorMessages.FieldRequired)]
		[StringLength(64, ErrorMessage = AttributeErrorMessages.StringMaximumLength)]
		public string FirstName { get; set; }

		[Display(Name = AttributeNames.LastName)]
		[Required(ErrorMessage = AttributeErrorMessages.FieldRequired)]
		[StringLength(64, ErrorMessage = AttributeErrorMessages.StringMaximumLength)]
		public string LastName { get; set; }

		[Display(Name = AttributeNames.FullName)]
		public string FullName { get => $"{FirstName} {LastName}"; }

		[Display(Name = AttributeNames.PhoneNumber)]
		[Required(ErrorMessage = AttributeErrorMessages.FieldRequired)]
		[StringLength(16, ErrorMessage = AttributeErrorMessages.StringMaximumLength)]
		public string PhoneNumber { get; set; }

		[Display(Name = AttributeNames.EmailAddress)]
		[Required(ErrorMessage = AttributeErrorMessages.FieldRequired)]
		[StringLength(64, ErrorMessage = AttributeErrorMessages.StringMaximumLength)]
		[EmailAddress(ErrorMessage = AttributeErrorMessages.InvalidFormat)]
		public string EmailAddress { get; set; }

		[Display(Name = AttributeNames.IdentityNationalDocument)]
		[Required(ErrorMessage = AttributeErrorMessages.FieldRequired)]
		[StringLength(16, ErrorMessage = AttributeErrorMessages.StringMaximumLength)]
		public string IdentityNationalDocument { get; set; }

		[Display(Name = AttributeNames.RatePerKilometer)]
		[Required(ErrorMessage = AttributeErrorMessages.FieldRequired)]
		public double RatePerKilometer { get; set; }

		public ICollection<TripView> Trips { get; set; }
	}
}
