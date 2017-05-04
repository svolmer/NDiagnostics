using NDiagnostics.Metering.Samples;
using NDiagnostics.Metering.Types;

namespace NDiagnostics.Metering
{
    public interface IMultiTimer : IMeter<MultiTimerSample>
    {
        void Sample(Time elapsedTimeOfActivity);
    }
}
