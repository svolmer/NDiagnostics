using NDiagnostics.Metering.Samples;
using NDiagnostics.Metering.Types;

namespace NDiagnostics.Metering
{
    public interface ITimerInverse : IMeter<TimerInverseSample>
    {
        void Sample(Time elapsedTimeOfInactivity);
    }
}
