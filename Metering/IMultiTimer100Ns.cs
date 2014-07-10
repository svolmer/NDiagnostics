using NDiagnostics.Metering.Samples;
using NDiagnostics.Metering.Types;

namespace NDiagnostics.Metering
{
    public interface IMultiTimer100Ns : IMeter
    {
        void Sample(Time100Ns elapsedTimeOfActivity);

        new MultiTimer100NsSample Current { get; }
    }
}
