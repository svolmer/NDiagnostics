using NDiagnostics.Metering.Samples;

namespace NDiagnostics.Metering
{
    public interface IDifferentialValue : IMeter<DifferentialValueSample>
    {
        long Increment();

        long IncrementBy(long value);

        long Decrement();

        long DecrementBy(long value);

        void Set(long value);
    }
}
