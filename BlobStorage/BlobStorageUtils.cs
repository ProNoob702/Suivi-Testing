using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

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

        public static Bitmap? BuildThumbnailBitmap(BlobDownloadInfo sourceDownloadInfo, bool fill, int? maxX, int? maxY)
        {
            var image = MakeBitmapImg(sourceDownloadInfo);
            if (image != null)
            {
                var rx = maxX.HasValue ? (double)image.Width / maxX : 1;
                var ry = maxY.HasValue ? (double)image.Height / maxY : 1;
                var r = (rx > ry) ? rx : ry;
                if (r > 1 || fill)
                {
                    int newX;
                    int newY;
                    if (r > 1)
                    {
                        newX = (int)(image.Width / r);
                        newY = (int)(image.Height / r);
                    }
                    else
                    {
                        newX = image.Width;
                        newY = image.Height;
                    }
                    int x = 0;
                    int y = 0;
                    Bitmap newImage;
                    if (fill)
                    {
                        newImage = new Bitmap(maxX!.Value, maxY!.Value);
                        x = (maxX.Value - newX) / 2;
                        y = (maxY.Value - newY) / 2;
                    }
                    else
                    {
                        newImage = new Bitmap(newX, newY);
                    }
                    using (Graphics gr = Graphics.FromImage(newImage))
                    {
                        gr.SmoothingMode = SmoothingMode.HighQuality;
                        gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        gr.CompositingQuality = CompositingQuality.HighQuality;
                        gr.DrawImage(image, new Rectangle(x, y, newX, newY));
                    }
                    return newImage;
                }
                else
                {
                    return image;
                }
            }
            return null;
        }

        private static Bitmap? MakeBitmapImg(BlobDownloadInfo sourceDownloadInfo)
        {
            var contentType = sourceDownloadInfo.ContentType;
            Bitmap? img;
            try
            {
                switch (contentType)
                {
                    case "image/png":
                    case "image/jpeg":
                    case "image/gif":
                        {
                            img = new Bitmap(sourceDownloadInfo.Content);
                            break;
                        }
                    default:
                        img = null;
                        throw new ArgumentException(string.Format("Unknown contentType : {0}", contentType));
                }
            }
            catch
            {
                img = null;
            }
            return img;
        }
    }
}
