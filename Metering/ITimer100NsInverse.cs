using NDiagnostics.Metering.Samples;
using NDiagnostics.Metering.Types;

namespace NDiagnostics.Metering
{
    public interface ITimer100NsInverse : IMeter
    {
        void Sample(Time100Ns elapsedTimeOfInactivity);

        new Timer100NsInverseSample Current { get; }
    }
}
