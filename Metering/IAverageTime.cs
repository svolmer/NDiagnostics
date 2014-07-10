using NDiagnostics.Metering.Samples;
using NDiagnostics.Metering.Types;

namespace NDiagnostics.Metering
{
    public interface IAverageTime : IMeter
    {
        void Sample(Time elapsedTime);

        new AverageTimeSample Current { get; }
    }
}
