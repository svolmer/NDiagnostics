using System;

namespace NDiagnostics.Metering.Extensions
{
    internal static class StringExtensions
    {
        #region Methods

        internal static string ThrowIfNullOrEmpty(this string self, string name)
        {
            self.ThrowIfNull(name);
            if(string.IsNullOrEmpty(self))
            {
                throw new ArgumentException(string.Format("{0} '{1}' is empty.", self.Type().ToName(), name));
            }
            return self;
        }

        internal static string ThrowIfNullOrWhiteSpace(this string self, string name)
        {
            self.ThrowIfNullOrEmpty(name);
            if(string.IsNullOrWhiteSpace(self))
            {
                throw new ArgumentException(string.Format("{0} '{1}' consists only of white-space characters.", self.Type().ToName(), name));
            }
            return self;
        }

        internal static string ThrowIfExceedsMaxSize(this string self, string name, int maxSize)
        {
            if(self != null && self.Length > maxSize)
            {
                throw new ArgumentException(string.Format("{0} '{1}' exceeds the maximum length of {2} characters.", self.Type().ToName(), name, maxSize));
            }
            return self;
        }

        #endregion
    }
}
