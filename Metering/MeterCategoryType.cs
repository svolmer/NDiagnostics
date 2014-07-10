using System.Diagnostics;

namespace NDiagnostics.Metering
{
    public enum MeterCategoryType
    {
        MultiInstance = PerformanceCounterCategoryType.MultiInstance,

        SingleInstance = PerformanceCounterCategoryType.SingleInstance,
    }
}
