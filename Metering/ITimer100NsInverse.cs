using NDiagnostics.Metering.Samples;
using NDiagnostics.Metering.Types;

namespace NDiagnostics.Metering
{
    public interface ITimer100NsInverse : IMeter<Timer100NsInverseSample>
    {
        void Sample(Time100Ns elapsedTimeOfInactivity);
    }
}
