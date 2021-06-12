using Worktop.Core.Application.Models.File;

namespace Worktop.Core.Application.Services
{
    public interface IMimeMappingService
    {
        string MapContentType(DownloadContent downloadContent);
    }
}