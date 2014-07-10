using System;

namespace NDiagnostics.Metering.Extensions
{
    internal static class StringExtensions
    {
        #region Public Methods

        public static void ThrowIfNullOrEmpty(this string self, string name)
        {
            self.ThrowIfNull(name);

            if(string.IsNullOrEmpty(self))
            {
                throw new ArgumentException(string.Format("{0} '{1}' is empty.", self.Type().ToName(), name));
            }
        }

        public static void ThrowIfNullOrWhiteSpace(this string self, string name)
        {
            self.ThrowIfNullOrEmpty(name);

            if(string.IsNullOrWhiteSpace(self))
            {
                throw new ArgumentException(string.Format("{0} '{1}' consists only of white-space characters.", self.Type().ToName(), name));
            }
        }

        public static void ThrowIfExceedsMaxSize(this string self, string name, int maxSize)
        {
            self.ThrowIfNull(name);

            if(self.Length > maxSize)
            {
                throw new ArgumentException(string.Format("{0} '{1}' exceeds the maximum length of {2} characters.", self.Type().ToName(), name, maxSize));
            }
        }

        #endregion
    }
}
