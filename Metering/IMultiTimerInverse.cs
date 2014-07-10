using NDiagnostics.Metering.Samples;
using NDiagnostics.Metering.Types;

namespace NDiagnostics.Metering
{
    public interface IMultiTimerInverse : IMeter
    {
        void Sample(Time elapsedTimeOfInactivity);

        new MultiTimerInverseSample Current { get; }
    }
}
