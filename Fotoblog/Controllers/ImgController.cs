using Microsoft.AspNetCore.Mvc;

namespace Fotoblog.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ImgController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ImgController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("{folder}/{file}")]
        public async Task<IActionResult> Get([FromRoute]string folder, [FromRoute]string file)
        {
            var AppConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            string photosSaveFolder = AppConfig.GetSection("AppSettings:PhotosUpload:BaseSavePath").Value.ToString();

            string img = Path.Combine(_webHostEnvironment.ContentRootPath, photosSaveFolder, folder, file);

            if(System.IO.File.Exists(img))
            {
                byte[] bytes = await System.IO.File.ReadAllBytesAsync(img);
                return File(bytes, "image/png");
            }

            return NotFound();
        }
    }
}
