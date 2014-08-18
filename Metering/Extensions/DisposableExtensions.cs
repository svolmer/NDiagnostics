using System;
using System.Runtime;

namespace NDiagnostics.Metering.Extensions
{
    public static class DisposabletExtensions
    {
        #region Public Methods

        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        public static void ThrowIfDisposed(this IDisposableObject self)
        {
            self.ThrowIfNull();
            if(self.IsDisposed)
            {
                throw new ObjectDisposedException(string.Format("{0} is already disposed.", self.Type().ToName()), (Exception) null);
            }
        }

        #endregion
    }
}
