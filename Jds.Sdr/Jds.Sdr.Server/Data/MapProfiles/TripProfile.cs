using AutoMapper;
using Jds.Sdr.Server.Data.Entities;
using Jds.Sdr.Shared.Data.EntityViews;

namespace Jds.Sdr.Server.Data.MapProfiles
{
	public class TripProfile : Profile
	{
		public TripProfile()
		{
			CreateMap<Trip, TripView>().ReverseMap();
		}
	}
}
