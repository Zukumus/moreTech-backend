using AutoMapper;
using Newscatcher.Client.Contracts.Models;
using NewsCatcher.Repositories.Contracts.Models;

namespace MoreTech.MapperProfiles;

public class NewsCatcherMapperProfile : Profile
{
    public NewsCatcherMapperProfile()
    {
        CreateMap<NewsModel, NewsRepositoryModel>()
            .ForMember(i => i.PublishDate, o => o.MapFrom(s => DateTime.Parse(s.PublishDate)));
    }
}