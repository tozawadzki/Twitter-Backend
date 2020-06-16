namespace PGSTwitter.Services
{
    using AutoMapper;
    using Models;
    using Repositories.Models;

    public class MappingProfile : Profile 
    {
        public MappingProfile()
        {
            CreateMap<TwitterUser, UserInfoResponse>();
            CreateMap<UserInfoResponse, TwitterUser>();
            CreateMap<UserCreateRequest, TwitterUser>()
                .ForMember(tu => tu.UserName, opt => opt.MapFrom(src => src.Email));

            CreateMap<TweetRequest, Tweet>()
                .ForMember(t => t.TweetContent, opt => opt.MapFrom(dto => dto.Content));
            CreateMap<TweetResponse, Tweet>()
                .ForMember(t => t.TweetContent, opt => opt.MapFrom(dto => dto.Content))
                .ForMember(t => t.LastTimeOfEdit, opt => opt.MapFrom(dto => dto.LastEditDate));
            CreateMap<Tweet, TweetResponse>()
                .ForMember(dto => dto.Content, opt => opt.MapFrom(t => t.TweetContent))
                .ForMember(dto => dto.UserFirstName, opt => opt.MapFrom(t => t.User.FirstName))
                .ForMember(dto => dto.UserLastName, opt => opt.MapFrom(t => t.User.LastName))
                .ForMember(dto => dto.LastEditDate, opt => opt.MapFrom(t => t.LastTimeOfEdit));
        }
    }
}
