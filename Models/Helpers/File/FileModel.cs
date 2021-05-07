namespace Worktop.Models.Helpers.File
{
    public class FileModel
    {
        public string Path { get; }
        public string Url { get; }
        public uint Size { get; set; }
        public string FullPath { get; }

        public FileModel(string path, string url, uint size, string fullPath = null)
        {
            Path = path;
            Url = url;
            Size = size;
            FullPath = fullPath;
        }
    }
}