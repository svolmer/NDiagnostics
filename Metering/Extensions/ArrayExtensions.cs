using System;
using System.Collections;

namespace NDiagnostics.Metering.Extensions
{
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
