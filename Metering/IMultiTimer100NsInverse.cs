using NDiagnostics.Metering.Samples;
using NDiagnostics.Metering.Types;

namespace NDiagnostics.Metering
{
    public interface IMultiTimer100NsInverse : IMeter<MultiTimer100NsInverseSample>
    {
        void Sample(Time100Ns elapsedTimeOfInactivity);
    }
}
