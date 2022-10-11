using Fotoblog.BLL.Services.PhotosService;
using Fotoblog.Utils.ViewModels.Photos;
using Fotoblog.Utils.ViewModels.Tags;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;

namespace Fotoblog.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PhotosController : BaseController
    {
        private readonly IPhotoService _photoService;

        public PhotosController(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        [HttpPost]
        public async Task<IActionResult> AddNew([FromForm] NewPhotoDataVm newPhotoDataVm)
        {
            var result = await _photoService.AddPhoto(newPhotoDataVm);
            return FromResult(result);
        }

        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var result = await _photoService.GetAll();
            return FromResult(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            var result = await _photoService.Delete(id);
            return FromResult(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] PhotoVm updatePhoto)
        {
            var result = await _photoService.Update(updatePhoto);
            return FromResult(result);
        }
    }
}
