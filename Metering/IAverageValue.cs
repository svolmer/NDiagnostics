using NDiagnostics.Metering.Samples;

namespace NDiagnostics.Metering
{
    public interface IAverageValue : IMeter
    {
        void Sample(long value);

        new AverageValueSample Current { get; }
    }
}
