using DevExpress.Blazor.Internal;
using Jds.Sdr.Shared.Data.DataAnnotations;
using Jds.Sdr.Shared.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Jds.Sdr.Shared.Data.EntityViews
{
	public class TripView : ClonableEquatable<TripView>, IDataEntity<int>
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = AttributeErrorMessages.FieldRequired)]
		public int? BranchId { get; set; }

		[Display(Name = AttributeNames.Branch)]
		public BranchView Branch { get; set; }

		[Required(ErrorMessage = AttributeErrorMessages.FieldRequired)]
		public int? CarrierId { get; set; }

		[Display(Name = AttributeNames.Carrier)]
		public CarrierView Carrier { get; set; }

		[Required(ErrorMessage = AttributeErrorMessages.FieldRequired)]
		public int CreatorId { get; set; }

		[Display(Name = AttributeNames.Creator)]
		public EmployeeView Creator { get; set; }

		[Display(Name = AttributeNames.Date)]
		[Required(ErrorMessage = AttributeErrorMessages.FieldRequired)]
		public DateTime Date { get; set; }

		[Display(Name = AttributeNames.TotalDistanceInKilometers)]
		public double TotalDistanceInKilometers
		{
			get
			{
				double totalDistance = 0.0D;
				TripDistances?.ForEach(x => totalDistance += x.Distance.DistanceInKilometers);
				return totalDistance;
			}
		}

		[Display(Name = AttributeNames.TotalAmount)]
		public double TotalAmount
		{
			get
			{
				if (Carrier is null)
					return 0.0D;
				double totalDistance = 0.0D;
				TripDistances?.ForEach(x => totalDistance += x.Distance.DistanceInKilometers);
				return totalDistance * Carrier.RatePerKilometer;
			}
		}

		[Display(Name = AttributeNames.EmployeeCount)]
		public int EmployeeCount
		{
			get
			{
				return TripDistances is not null ? TripDistances.Count : 0;
			}
		}

		public ICollection<TripDistanceView> TripDistances { get; set; }
	}
}
