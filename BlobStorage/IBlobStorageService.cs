using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Domain;
using Azure.Storage.Blobs.Models;

namespace BlobStorage
{
    public interface IBlobStorageService
    {
        public Task<IFileDescriptor> GetBlobMetaDataAsync(string fileId);

        //  Task<HttpResponseMessage> GetFileAsHTTPResponse(FileRef id, ContentDisposition contentDisposition, RangeHeaderValue? rangeHeader);
        public Task<IEnumerable<IFileDescriptor>?> AddFilesFromHTTPRequestAsync(IEnumerable<IFormFile> request);
        public Task<IFileDescriptor?> AddFileFromStream(Stream file, long size, string fileName, string? fileId);
        public Task<BlobDownloadInfo?> GetFileStreamAsync(string id);
        public Task<Stream?> GetThumbnailStreamAsync(string id, bool fill, int? x, int? y);
        public string GetFileNameMetaDataAttribut();
    }
}
