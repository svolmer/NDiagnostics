using NDiagnostics.Metering.Samples;
using NDiagnostics.Metering.Types;

namespace NDiagnostics.Metering
{
    public interface IMultiTimer100NsInverse : IMeter
    {
        void Sample(Time100Ns elapsedTimeOfInactivity);

        new MultiTimer100NsInverseSample Current { get; }
    }
}
