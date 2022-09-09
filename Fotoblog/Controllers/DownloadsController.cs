using Microsoft.AspNetCore.Mvc;

namespace Fotoblog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DownloadsController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public DownloadsController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("{photosFolder}/{uniqueName}/{file}")]
        public async Task<IActionResult> Get(
            [FromRoute] string photosFolder,
            [FromRoute] string uniqueName,
            [FromRoute] string file)
        {
            string img = Path.Combine(_webHostEnvironment.ContentRootPath, photosFolder, uniqueName, file);

            if(System.IO.File.Exists(img))
            {
                byte[] bytes = await System.IO.File.ReadAllBytesAsync(img);
                return File(bytes, "image/png");
            }

            return NotFound();
        }
    }
}
