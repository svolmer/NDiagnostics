using System.Diagnostics;
using System.Runtime;

namespace NDiagnostics.Metering.Extensions
{
    [DebuggerStepThrough]
    internal static class FloatExtensions
    {
        #region Methods

        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        internal static long Round(this double self)
        {
            return (long)(self + (self >= 0.0F ? 0.5 : -0.5));
        }

        #endregion
    }
}
