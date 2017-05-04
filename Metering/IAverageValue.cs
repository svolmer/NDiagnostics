using NDiagnostics.Metering.Samples;

namespace NDiagnostics.Metering
{
    public interface IAverageValue : IMeter<AverageValueSample>
    {
        void Sample(long value);
    }
}
