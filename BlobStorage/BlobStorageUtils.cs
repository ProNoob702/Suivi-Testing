using Microsoft.AspNetCore.StaticFiles;

namespace BlobStorage
{
    public static class BlobStorageUtils
    {
        public static string GetContentType(string fileName)
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            if (!provider.TryGetContentType(fileName, out contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }
    }
}
