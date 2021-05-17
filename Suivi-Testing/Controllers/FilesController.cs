using BlobStorage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Statsh.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        public IBlobStorageService _blobStorageService { get; }
        public FilesController(IBlobStorageService blobStorageService)
        {
            _blobStorageService = blobStorageService;
        }

        //Task<ActionResult<IEnumerable<BlobStorage.IFileDescriptor>>> 
        public async Task<IEnumerable<BlobStorage.IFileDescriptor>?> Post([FromForm] List<IFormFile> files)
        {
            // Lire https://docs.microsoft.com/en-us/aspnet/core/mvc/models/file-uploads?view=aspnetcore-5.0&viewFallbackFrom=aspnetcore-2.0
            //throw new NotImplementedException();
            //if (files == null || files.Count > 0)
            //{
            return await _blobStorageService.AddFilesFromHTTPRequestAsync(files);
            //}
            //else
            //{
            //    return BadRequest("The uploaded files empty");
            //}
        }
    }
}
