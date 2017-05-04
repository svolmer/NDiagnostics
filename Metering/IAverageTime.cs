using NDiagnostics.Metering.Samples;
using NDiagnostics.Metering.Types;

namespace NDiagnostics.Metering
{
    public interface IAverageTime : IMeter<AverageTimeSample>
    {
        void Sample(Time elapsedTime);
    }
}
