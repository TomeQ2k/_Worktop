namespace Worktop.Models.Helpers.File
{
    public class FileModel
    {
        public string Path { get; }
        public string Url { get; }
        public uint Size { get; set; }

        public FileModel(string path, string url, uint size)
        {
            Path = path;
            Url = url;
            Size = size;
        }
    }
}