using System.Diagnostics;

namespace NDiagnostics.Metering
{
    public enum InstanceLifetime
    {
        Global = PerformanceCounterInstanceLifetime.Global,

        Process = PerformanceCounterInstanceLifetime.Process,

        Managed = 2,
    }
}
