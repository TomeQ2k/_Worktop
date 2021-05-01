using Worktop.Core.Services;

namespace Worktop.ViewModels
{
    public class NumberErrorViewModel : ErrorBaseViewModel<int>
    {
        public override ErrorBaseViewModel<int> WithError(int key)
        {
            Error = ErrorWriter<int>.Error;

            return base.WithError(key);
        }
    }
}