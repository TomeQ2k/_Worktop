using System.Collections.Generic;
using Worktop.Core.Services;

namespace Worktop.ViewModels
{
    public abstract class ErrorBaseViewModel : BaseViewModel
    {
        public string Error { get; protected set; }

        public virtual ErrorBaseViewModel WithError()
        {
            Error = ErrorWriter.Error;
            Alert = Alertify.Alert;

            ErrorWriter.Clear();
            Alertify.Clear();

            return this;
        }
    }

    public abstract class ErrorBaseViewModel<T> : BaseViewModel
    {
        public KeyValuePair<dynamic, string> Error { get; protected set; }

        public virtual ErrorBaseViewModel<T> WithError(T key)
        {
            Error = ErrorWriter<T>.Error;
            Alert = Alertify.Alert;

            ErrorWriter<T>.Clear(key);
            Alertify.Clear();

            return this;
        }
    }
}