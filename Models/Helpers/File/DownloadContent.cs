using System.Collections.Generic;

namespace Worktop.Models.Helpers.File
{
    public class DownloadContent
    {
        public byte[] File { get; }
        public string FileName { get; }

        public Dictionary<string, string> ContentTypesDictionary
           => new Dictionary<string, string>
           {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
           };

        public DownloadContent(byte[] file, string fileName)
        {
            File = file;
            FileName = fileName;
        }
    }
}