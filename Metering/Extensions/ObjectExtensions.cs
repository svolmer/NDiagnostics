using System;
using System.Diagnostics;
using System.Runtime;

namespace NDiagnostics.Metering.Extensions
{
    [DebuggerStepThrough]
    internal static class ObjectExtensions
    {
        #region Methods

        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        internal static bool IsNull<T>(this T self)
        {
            return ReferenceEquals(null, self);
        }

        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        internal static bool IsNotNull<T>(this T self)
        {
            return !ReferenceEquals(null, self);
        }

        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        internal static Type Type<T>(this T self)
        {
            return self.IsNull() ? typeof(T) : self.GetType();
        }

        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        internal static T ThrowIfNull<T>(this T self)
        {
            if(self.IsNull())
            {
                throw new NullReferenceException($"{self.Type().ToName()} is null.");
            }
            return self;
        }

        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        internal static T ThrowIfNull<T>(this T self, string name)
        {
            if(self.IsNull())
            {
                throw new ArgumentNullException($"{self.Type().ToName()} '{name}' is null.", (Exception) null);
            }
            return self;
        }

        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        internal static T ThrowIfNull<T, TException>(this T self, TException innerException)
            where TException : Exception
        {
            if(self.IsNull())
            {
                throw innerException;
            }
            return self;
        }

        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        internal static T ThrowIfDisposed<T>(this T self)
        {
            var disposable = self as IDisposableObject;
            if (disposable != null && disposable.IsDisposed)
            {
                throw new ObjectDisposedException($"{self.Type().ToName()} is already disposed.", (Exception)null);
            }
            return self;
        }

        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        internal static T ThrowIfDisposed<T>(this T self, string name)
        {
            var disposable = self as IDisposableObject;
            if (disposable != null && disposable.IsDisposed)
            {
                throw new ObjectDisposedException($"{self.Type().ToName()} '{name}' is already disposed.", (Exception)null);
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
            catch (Exception) { }
        }

        #endregion
    }
}
