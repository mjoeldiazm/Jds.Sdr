using Jds.Sdr.Shared.Data.DataAnnotations;
using Jds.Sdr.Shared.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Jds.Sdr.Shared.Data.EntityViews
{
	public class BranchView : ClonableEquatable<BranchView>, IDataEntity<int>
	{
		[Key]
		public int Id { get; set; }

		[Display(Name = AttributeNames.Name)]
		[Required(ErrorMessage = AttributeErrorMessages.FieldRequired)]
		[StringLength(256, ErrorMessage = AttributeErrorMessages.StringMaximumLength)]
		public string Name { get; set; }

		[Display(Name = AttributeNames.Location)]
		[Required(ErrorMessage = AttributeErrorMessages.FieldRequired)]
		[StringLength(256, ErrorMessage = AttributeErrorMessages.StringMaximumLength)]
		public string Location { get; set; }

		public ICollection<DistanceView> BranchEmployees { get; set; }
	}
}
