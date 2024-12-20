using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HKCCinemas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageUploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public ImageUploadController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] IFormFile image)
        {
            if (image == null || image.Length == 0)
            {
                return BadRequest("No image uploaded.");
            }

            var filePath = Path.Combine(_env.ContentRootPath, "wwwroot/images", image.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            var imageUrl = Url.Content($"~/images/{image.FileName}");
            return Ok(new { url = imageUrl });
        }
    }
}
