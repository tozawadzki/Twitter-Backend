namespace PGSTwitter.Services.UserModels
{
    using AutoMapper;
    using Repositories.Models;

    public class UserMappingProfile : Profile 
    {
        public UserMappingProfile()
        {
            CreateMap<TwitterUser, UserDTO>();
            CreateMap<UserDTO, TwitterUser>();
            CreateMap<NewUserDTO, TwitterUser>()
                .ForMember(tu => tu.UserName,
                    opt => opt.MapFrom(src => src.Email));
        }
    }
}
