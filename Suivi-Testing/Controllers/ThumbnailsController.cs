using BlobStorage;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace Suivi_Testing.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ThumbnailsController : ControllerBase
    {
        public IBlobStorageService _blobStorageService { get; }
        public ThumbnailsController(IBlobStorageService blobStorageService)
        {
            _blobStorageService = blobStorageService;
        }

        [HttpGet("{fileId}")]
        public async Task<IActionResult> GetThumbnail(string fileId, [FromQuery] int? x = null, [FromQuery] int? y = null)
        {
            var thumbStream = await _blobStorageService.GetThumbnailStreamAsync(fileId, false, x, y);
            if (thumbStream != null)
            {
                return File(thumbStream, "image/jpeg");
            }
            else
            {
                return BadRequest("Thumnbail can't be created");
            }
        }

        [HttpGet("fill/{fileId}")]
        public async Task<IActionResult> GetFilledThumbnail(string fileId, [FromQuery] int? x = null, [FromQuery] int? y = null)
        {
            if (x == null)
            {
                return BadRequest("x is required");
            }
            if (y == null)
            {
                return BadRequest("y is required");
            }
            var thumbStream = await _blobStorageService.GetThumbnailStreamAsync(fileId, true, x, y);
            if (thumbStream != null)
            {
                return File(thumbStream, "image/jpeg");
            }
            else
            {
                return BadRequest("Thumnbail can't be created");
            }

        }
    }
}
