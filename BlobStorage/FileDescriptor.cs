namespace BlobStorage
{
    public class FileDescriptor : IFileDescriptor
    {
        public string Id { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public long Size { get; set; }
    }

    public interface IFileDescriptor
    {
        string Id { get; set; }
        string FileName { get; set; }
        string ContentType { get; set; }
        long Size { get; set; }
    }
}
