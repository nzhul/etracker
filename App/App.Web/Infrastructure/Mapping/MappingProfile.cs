using App.Models;
using App.Models.InputModels;
using AutoMapper;

namespace App.Web.Infrastructure.Mapping
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<ApplicationUser, EditClientInputModel>().ForMember(x => x.Phone, opt => opt.MapFrom(u => u.PhoneNumber));

			CreateMap<EditClientInputModel, ApplicationUser>()
				.ForMember(x => x.PhoneNumber, opt => opt.MapFrom(u => u.Phone))
				.ForMember(x => x.ProfileImage, opt => opt.Ignore());
		}
	}
}