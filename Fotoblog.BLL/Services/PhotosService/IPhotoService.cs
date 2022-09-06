using Fotoblog.BLL.Services.ServiceResultNS;
using Fotoblog.Utils.ViewModels.Photos;

namespace Fotoblog.BLL.Services.PhotosService
{
    public interface IPhotoService
    {
        Task<ServiceResult> AddPhoto(NewPhotoDataVm newPhotoDataVm);
        public void SetLocation(string contentRootPath);
    }
}