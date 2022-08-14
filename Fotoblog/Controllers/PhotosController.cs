using Fotoblog.BLL.Services.PhotosService;
using Fotoblog.Utils.ViewModels.Photos;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> AddNew([FromBody] NewPhotoVm newPhotoVm)
        {
            var result = await _photoService.AddPhoto(newPhotoVm);
            return FromResult(result);
        }
    }
}
