using Fotoblog.BLL.Services.ServiceResultNS;
using Fotoblog.Utils.ViewModels.Tags;

namespace Fotoblog.BLL.Services.TagService
{
    public interface ITagService
    {
        Task<ServiceResult> AddNewTag(NewTagVm createTagVm);
        Task<ServiceResult<List<TagVm>>> GetAllTags();
        Task<ServiceResult> UpdateTag(UpdateTagVm updateTagVm);
        Task<ServiceResult> RemoveTag(RemoveTagVm removeTagVm);
    }
}