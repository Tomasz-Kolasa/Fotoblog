using Fotoblog.BLL.Services.ServiceResultNS;
using Fotoblog.Utils.ViewModels.Tags;

namespace Fotoblog.BLL.Services.TagService
{
    public interface ITagService
    {
        Task<ServiceResult> AddNew(NewTagVm createTagVm);
        Task<ServiceResult<List<TagVm>>> GetAll();
        Task<ServiceResult> Update(UpdateTagVm updateTagVm);
        Task<ServiceResult> Remove(int tagId);
    }
}