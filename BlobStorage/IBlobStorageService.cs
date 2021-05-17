using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Domain;

namespace BlobStorage
{
    public interface IBlobStorageService
    {
        public Task<IFileDescriptor> GetBlobMetaDataAsync(FileRef fileId);

        //  Task<HttpResponseMessage> GetFileAsHTTPResponse(FileRef id, ContentDisposition contentDisposition, RangeHeaderValue? rangeHeader);
        public Task<IEnumerable<IFileDescriptor>?> AddFilesFromHTTPRequestAsync(IEnumerable<IFormFile> request);
        public Task<IFileDescriptor?> AddFileFromStream(Stream file, long size, string fileName);
        public Task<HttpResponseMessage> GetFileAsHTTPResponseAsync(FileRef id, ContentDisposition contentDisposition, RangeHeaderValue rangeHeader);
        public Task<HttpResponseMessage> GetThumbnailAsHTTPResponseAsync(string id, bool fill, int? x, int? y);
    }
}
