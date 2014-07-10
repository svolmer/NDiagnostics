using NDiagnostics.Metering.Samples;
using NDiagnostics.Metering.Types;

namespace NDiagnostics.Metering
{
    public interface IMultiTimer : IMeter
    {
        void Sample(Time elapsedTimeOfActivity);

        new MultiTimerSample Current { get; }
    }
}
