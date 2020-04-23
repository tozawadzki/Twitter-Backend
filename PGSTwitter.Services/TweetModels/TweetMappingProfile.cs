namespace PGSTwitter.Services.TweetModels
{
    using AutoMapper;
    using Repositories.Models;

    public class TweetMappingProfile : Profile 
    {
        public TweetMappingProfile()
        {
            CreateMap<NewTweetDTO, Tweet>()
                .ForMember(t => t.TweetContent,
                    opt => opt.MapFrom(dto => dto.Content));
            CreateMap<TweetDTO, Tweet>()
                .ForMember(t => t.TweetContent,
                    opt => opt.MapFrom(dto => dto.Content))
                .ForMember(t => t.LastTimeOfEdit,
                    opt => opt.MapFrom(dto => dto.LastEditDate));
            CreateMap<Tweet, TweetDTO>()
                .ForMember(dto => dto.Content,
                    opt => opt.MapFrom(t => t.TweetContent))
                .ForMember(dto => dto.UserFirstName,
                    opt => opt.MapFrom(t => t.User.FirstName))
                .ForMember(dto => dto.UserLastName,
                    opt => opt.MapFrom(t => t.User.LastName))
                .ForMember(dto => dto.LastEditDate,
                    opt => opt.MapFrom(t => t.LastTimeOfEdit));
        }
    }
}
