using Fotoblog.BLL.Services.PhotosService;
using Fotoblog.Utils.ViewModels.Photos;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;

namespace Fotoblog.Controllers
{
    public class PhotosController : BaseController
    {
        private readonly IPhotoService _photoService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IServer _server;

        public PhotosController(IPhotoService photoService,
            IWebHostEnvironment webHostEnvironment,
            IServer server)
        {
            _webHostEnvironment = webHostEnvironment;
            _photoService = photoService;
            _server = server;
            //var addresses = _server.Features.Get<IServerAddressesFeature>().Addresses;
            _photoService.SetLocation(_webHostEnvironment.ContentRootPath);
        }

        [HttpPost]
        public async Task<IActionResult> AddNew([FromForm] NewPhotoDataVm newPhotoDataVm)
        {
            var result = await _photoService.AddPhoto(newPhotoDataVm);
            return FromResult(result);
        }

        [HttpGet]
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
    }
}
