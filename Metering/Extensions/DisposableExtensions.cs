using System;
using System.Runtime;

namespace NDiagnostics.Metering.Extensions
{
    public static class DisposabletExtensions
    {
        #region Methods

        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        internal static T ThrowIfDisposed<T>(this T self)
        {
            var disposable = self as IDisposableObject;
            if(disposable != null && disposable.IsDisposed)
            {
                throw new ObjectDisposedException(String.Format("{0} is already disposed.", self.Type().ToName()), (Exception) null);
            }
            return self;
        }

        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        internal static T ThrowIfDisposed<T>(this T self, string name)
        {
            var disposable = self as IDisposableObject;
            if(disposable != null && disposable.IsDisposed)
            {
                throw new ObjectDisposedException(String.Format("{0} '{1}' is already disposed.", self.Type().ToName(), name), (Exception) null);
            }
            return self;
        }

        internal static void TryDispose<T>(this T self)
        {
            var disposable = self as IDisposable;
            if(disposable == null)
            {
                return;
            }

            try
            {
                disposable.Dispose();
            }
            catch(Exception)
            {
            }
        }

        #endregion
    }
}
