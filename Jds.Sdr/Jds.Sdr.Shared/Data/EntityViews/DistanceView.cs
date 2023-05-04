using Jds.Sdr.Shared.Data.DataAnnotations;
using Jds.Sdr.Shared.Data.Validations;
using Jds.Sdr.Shared.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Jds.Sdr.Shared.Data.EntityViews
{
	public class DistanceView : ClonableEquatable<DistanceView>, IDataEntity<int>
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = AttributeErrorMessages.FieldRequired)]
		public int? BranchId { get; set; }

		[Display(Name = AttributeNames.Branch)]
		public BranchView Branch { get; set; }

		[Display(Name = AttributeNames.Employee)]
		public EmployeeView Employee { get; set; }

		[Required(ErrorMessage = AttributeErrorMessages.FieldRequired)]
		public int? EmployeeId { get; set; }

		[Display(Name = AttributeNames.DistanceInKilometers)]
		[Required(ErrorMessage = AttributeErrorMessages.FieldRequired)]
		[Range(NumericValidations.MinimumDistanceInKilometers,
			NumericValidations.MaximumDistanceInKilometers,
			ErrorMessage = AttributeErrorMessages.OutOfRange)]
		public double DistanceInKilometers { get; set; }
	}
}
