using NDiagnostics.Metering.Samples;
using NDiagnostics.Metering.Types;

namespace NDiagnostics.Metering
{
    public interface ITimer100Ns : IMeter<Timer100NsSample>
    {
        void Sample(Time100Ns elapsedTimeOfActivity);
    }
}
