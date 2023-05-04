using AutoMapper;
using Jds.Sdr.Server.Data.Entities;
using Jds.Sdr.Shared.Data.EntityViews;

namespace Jds.Sdr.Server.Data.MapProfiles
{
	public class UserAccountProfile : Profile
	{
		public UserAccountProfile()
		{
			CreateMap<UserAccount, UserAccountView>()
				.ForMember(dest => dest.EmailAddress, input => input.MapFrom(src => src.Email));
			CreateMap<UserAccountView, UserAccount>()
				.ForMember(dest => dest.UserName, input => input.MapFrom(src => src.EmailAddress))
				.ForMember(dest => dest.Email, input => input.MapFrom(src => src.EmailAddress));
		}
	}
}
