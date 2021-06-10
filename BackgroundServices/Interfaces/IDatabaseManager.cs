using System.Threading.Tasks;

namespace Worktop.BackgroundServices.Interfaces
{
    public interface IDatabaseManager
    {
        Task Seed();
    }
}