using NDiagnostics.Metering.Samples;
using NDiagnostics.Metering.Types;

namespace NDiagnostics.Metering
{
    public interface ITimer : IMeter<TimerSample>
    {
        void Sample(Time elapsedTimeOfActivity);
    }
}
