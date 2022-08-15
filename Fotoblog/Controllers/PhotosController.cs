using Fotoblog.BLL.Services.PhotosService;
using Fotoblog.BLL.Services.ServiceResultNS;
using Fotoblog.Utils.ViewModels.Photos;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Fotoblog.Controllers
{
    public class PhotosController : BaseController
    {
        private readonly IPhotoService _photoService;

        public PhotosController(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        [HttpPost]
        public async Task<IActionResult> AddNew(IFormCollection data, IFormFile imgFile)
        {
            NewPhotoDataVm? newPhotoDataVm = JsonSerializer.Deserialize<NewPhotoDataVm>(data["newPhotoData"]);

            if (newPhotoDataVm == null)
            {
                return FromResult( ServiceResult.Fail(ErrorCodes.GeneralError) );
            }

            newPhotoDataVm.file = imgFile;

            var result = await _photoService.AddPhoto(newPhotoDataVm);
            return FromResult(result);
        }
    }
}
