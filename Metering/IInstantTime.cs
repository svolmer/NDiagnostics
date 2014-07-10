using NDiagnostics.Metering.Samples;
using NDiagnostics.Metering.Types;

namespace NDiagnostics.Metering
{
    public interface IInstantTime : IMeter
    {
        void Set(TimeStamp start);

        new InstantTimeSample Current { get; }
    }
}
