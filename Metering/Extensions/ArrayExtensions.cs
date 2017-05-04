using System;
using System.Collections;
using System.Diagnostics;

namespace NDiagnostics.Metering.Extensions
{
    [DebuggerStepThrough]
    internal static class ArrayExtensions
    {
        #region Methods

        internal static ICollection ThrowIfEmpty<TException>(this ICollection self, TException exception)
            where TException : Exception
        {
            self.ThrowIfNull();

            if(self.Count < 1)
            {
                throw exception;
            }
            return self;
        }

        #endregion
    }
}
