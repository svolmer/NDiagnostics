using NDiagnostics.Metering.Samples;
using NDiagnostics.Metering.Types;

namespace NDiagnostics.Metering
{
    public interface ITimer100Ns : IMeter
    {
        void Sample(Time100Ns elapsedTimeOfActivity);

        new Timer100NsSample Current { get; }
    }
}
