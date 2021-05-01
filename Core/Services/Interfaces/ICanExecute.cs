using System.Threading.Tasks;

namespace Worktop.Core.Services.Interfaces
{
    public interface ICanExecute<T>
    {
        Task<bool> CanExecute(T item);
    }
}