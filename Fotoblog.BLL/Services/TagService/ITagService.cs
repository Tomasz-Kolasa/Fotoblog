using Fotoblog.BLL.Services.ServiceResultNS;
using Fotoblog.Utils.ViewModels.Tags;

namespace Fotoblog.BLL.Services.TagService
{
    public interface ITagService
    {
        Task<ServiceResult> AddNewTag(CreateTagVm createTagVm);
        Task<ServiceResult<List<TagVm>>> GetAllTags();
    }
}