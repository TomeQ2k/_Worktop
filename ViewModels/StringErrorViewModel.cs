using Worktop.Core.Services;

namespace Worktop.ViewModels
{
    public class StringErrorViewModel : ErrorBaseViewModel<string>
    {
        public override ErrorBaseViewModel<string> WithError(string key)
        {
            Error = ErrorWriter<string>.Error;

            return base.WithError(key);
        }
    }
}