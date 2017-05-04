using System;
using System.Diagnostics;

namespace NDiagnostics.Metering.Extensions
{
    [DebuggerStepThrough]
    internal static class StringExtensions
    {
        #region Methods

        internal static string ThrowIfNullOrEmpty(this string self, string name)
        {
            self.ThrowIfNull(name);
            if(string.IsNullOrEmpty(self))
            {
                throw new ArgumentException($"{self.Type().ToName()} '{name}' is empty.");
            }
            return self;
        }

        internal static string ThrowIfNullOrWhiteSpace(this string self, string name)
        {
            self.ThrowIfNullOrEmpty(name);
            if(string.IsNullOrWhiteSpace(self))
            {
                throw new ArgumentException($"{self.Type().ToName()} '{name}' consists only of white-space characters.");
            }
            return self;
        }

        internal static string ThrowIfExceedsMaxSize(this string self, string name, int maxSize)
        {
            if(self != null && self.Length > maxSize)
            {
                throw new ArgumentException($"{self.Type().ToName()} '{name}' exceeds the maximum length of {maxSize} characters.");
            }
            return self;
        }

        #endregion
    }
}
