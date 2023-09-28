using authentication_demo.Models;
using authentication_demo.ViewModels;
using authentication_demo.ViewModels.CreateModels;
using AutoMapper;

namespace authentication_demo.Profiles;
public class MapperProfiles : Profile
{
    public MapperProfiles()
    {
        CreateMap<AppUser, CreateUserModel>().ReverseMap()
            .ForMember(x => x.Id, opt => opt.MapFrom(x => Guid.NewGuid()))
            .ForMember(x => x.UserName, opt => opt.MapFrom(x => x.Email));
        CreateMap<AppUser, UserModel>().ReverseMap();    
            
    }
}