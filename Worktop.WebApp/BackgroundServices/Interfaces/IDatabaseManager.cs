using System.Threading.Tasks;

namespace Worktop.WebApp.BackgroundServices.Interfaces
{
    public interface IDatabaseManager
    {
        Task Seed();
    }
}