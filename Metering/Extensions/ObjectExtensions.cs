using System;
using System.Runtime;

namespace NDiagnostics.Metering.Extensions
{
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
                throw new NullReferenceException(string.Format("{0} is null.", self.Type().ToName()));
            }
            return self;
        }

        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        internal static T ThrowIfNull<T>(this T self, string name)
        {
            if(self.IsNull())
            {
                throw new ArgumentNullException(string.Format("{0} '{1}' is null.", self.Type().ToName(), name), (Exception) null);
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

        #endregion
    }
}
