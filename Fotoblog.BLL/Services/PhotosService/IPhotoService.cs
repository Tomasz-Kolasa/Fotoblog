using Fotoblog.BLL.Services.ServiceResultNS;
using Fotoblog.DAL.Entities;
using Fotoblog.Utils.ViewModels.Photos;

namespace Fotoblog.BLL.Services.PhotosService
{
    public interface IPhotoService
    {
        Task<ServiceResult> AddPhoto(NewPhotoDataVm newPhotoDataVm);

        Task<ServiceResult<List<PhotoVm>>> GetAll();
        Task<ServiceResult> Delete(int id);
        Task<ServiceResult> Update(PhotoVm updatePhotoVm);
    }
}