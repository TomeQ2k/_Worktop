using System.Threading.Tasks;

namespace Worktop.Core.Application.Services
{
    public interface ICanExecute<T>
    {
        Task<bool> CanExecute(T item);
    }
}