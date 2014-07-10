using NDiagnostics.Metering.Samples;
using NDiagnostics.Metering.Types;

namespace NDiagnostics.Metering
{
    public interface ITimerInverse : IMeter
    {
        void Sample(Time elapsedTimeOfInactivity);

        new TimerInverseSample Current { get; }
    }
}
