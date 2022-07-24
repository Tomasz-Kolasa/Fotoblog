using AutoMapper;
using Fotoblog.DAL.Entities;
using Fotoblog.Utils.ViewModels.Tags;

namespace Fotoblog.Utils.AutoMapper
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<UpdateTagVm, TagEntity>().ReverseMap();
            CreateMap<TagVm, TagEntity>().ReverseMap();
            CreateMap<NewTagVm, TagEntity>().ReverseMap();
        }
    }
}
