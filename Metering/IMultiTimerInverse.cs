using NDiagnostics.Metering.Samples;
using NDiagnostics.Metering.Types;

namespace NDiagnostics.Metering
{
    public interface IMultiTimerInverse : IMeter<MultiTimerInverseSample>
    {
        void Sample(Time elapsedTimeOfInactivity);
    }
}
