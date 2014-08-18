using System.Diagnostics;

namespace NDiagnostics.Metering
{
    public enum MeterInstanceLifetime
    {
        Global = PerformanceCounterInstanceLifetime.Global,

        Process = PerformanceCounterInstanceLifetime.Process,

        Managed = 2,
    }
}
