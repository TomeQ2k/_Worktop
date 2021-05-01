using Worktop.Models.Helpers.File;

namespace Worktop.Core.Services.Interfaces
{
    public interface IMimeMappingService
    {
        string MapContentType(DownloadContent downloadContent);
    }
}