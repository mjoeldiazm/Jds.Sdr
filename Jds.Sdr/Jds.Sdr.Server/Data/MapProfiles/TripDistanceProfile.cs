using AutoMapper;
using Jds.Sdr.Server.Data.Entities;
using Jds.Sdr.Shared.Data.EntityViews;

namespace Jds.Sdr.Server.Data.MapProfiles
{
	public class TripDistanceProfile : Profile
	{
		public TripDistanceProfile()
		{
			CreateMap<TripDistance, TripDistanceView>().ReverseMap();
		}
	}
}
