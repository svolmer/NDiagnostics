using NDiagnostics.Metering.Samples;
using NDiagnostics.Metering.Types;

namespace NDiagnostics.Metering
{
    public interface ITimer : IMeter
    {
        void Sample(Time elapsedTimeOfActivity);

        new TimerSample Current { get; }
    }
}
