using Jds.Sdr.Shared.Data.DataAnnotations;
using Jds.Sdr.Shared.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Jds.Sdr.Shared.Data.EntityViews
{
	public class TripDistanceView : ClonableEquatable<TripDistanceView>, IDataEntity<int>
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = AttributeErrorMessages.FieldRequired)]
		public int? TripId { get; set; }

		[Display(Name = AttributeNames.Trip)]
		public TripView Trip { get; set; }

		[Required(ErrorMessage = AttributeErrorMessages.FieldRequired)]
		public int? DistanceId { get; set; }

		[Display(Name = AttributeNames.Employee)]
		public DistanceView Distance { get; set; }
	}
}
