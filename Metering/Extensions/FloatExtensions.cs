using System.Runtime;

namespace NDiagnostics.Metering.Extensions
{
    internal static class FloatExtensions
    {
        #region Methods

        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        internal static long Round(this float self)
        {
            return (long) (self + (self >= 0.0F ? 0.5F : -0.5F));
        }

        #endregion
    }
}
