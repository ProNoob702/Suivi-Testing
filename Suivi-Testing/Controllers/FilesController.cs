using BlobStorage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Statsh.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        public IBlobStorageService _blobStorageService { get; }
        public FilesController(IBlobStorageService blobStorageService)
        {
            _blobStorageService = blobStorageService;
        }

        public class RequestModel
        {
            public IEnumerable<IFormFile> Files { get; set; }
        }

        // check out : https://stackoverflow.com/questions/38897764/asp-net-core-content-disposition-attachment-inline
        [HttpGet("{fileId}/{contentDisposition}")]
        public async Task<IActionResult> GetFile([FromRoute] string fileId, [FromRoute] ContentDisposition contentDisposition)
        {
            var downloadFileInfo = await _blobStorageService.GetFileStreamAsync(fileId, contentDisposition);
            if (downloadFileInfo != null)
            {
                if (contentDisposition == ContentDisposition.Attachment)
                {
                    var fileName = downloadFileInfo.Details.Metadata[_blobStorageService.GetFileNameMetaDataAttribut()];
                    return File(downloadFileInfo.Content, downloadFileInfo.ContentType, fileName);
                }
                else
                {
                    return File(downloadFileInfo.Content, downloadFileInfo.ContentType);
                }
            }
            else
            {
                return BadRequest("File Not Found");
            }
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<IFileDescriptor>?>> Post([FromForm] RequestModel request)
        {
            if (request.Files == null || request.Files.Any())
            {
                var filesDescriptors = await _blobStorageService.AddFilesFromHTTPRequestAsync(request.Files);
                return Ok(filesDescriptors);
            }
            else
            {
                return BadRequest("The uploaded files empty");
            }
        }
    }
}
