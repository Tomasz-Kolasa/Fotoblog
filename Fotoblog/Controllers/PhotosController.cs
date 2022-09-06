using Fotoblog.BLL.Services.PhotosService;
using Fotoblog.Utils.ViewModels.Photos;
using Microsoft.AspNetCore.Mvc;

namespace Fotoblog.Controllers
{
    public class PhotosController : BaseController
    {
        private readonly IPhotoService _photoService;

        public PhotosController(IPhotoService photoService, IWebHostEnvironment webHostEnvironment)
        {
            _photoService = photoService;
            _photoService.SetLocation(webHostEnvironment.ContentRootPath);
        }

        [HttpPost]
        public async Task<IActionResult> AddNew([FromForm] NewPhotoDataVm newPhotoDataVm)
        {
            var result = await _photoService.AddPhoto(newPhotoDataVm);
            return FromResult(result);
        }
    }
}
