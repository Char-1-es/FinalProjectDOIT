using AutoMapper;
using FinalProjectDOIT.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        CreateMap<FinalProjectDOIT.Entities.Topic, TopicDTO>();
        CreateMap<TopicDTO, FinalProjectDOIT.Entities.Topic>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        CreateMap<FinalProjectDOIT.Entities.Topic, TopicDetailDTO>();
        CreateMap<Comment, CommentDTO>();
        _ = CreateMap<CommentDTO, Comment>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}