using NDiagnostics.Metering.Samples;

namespace NDiagnostics.Metering
{
    public interface IDifferentialValue : IMeter
    {
        long Increment();

        long IncrementBy(long value);

        long Decrement();

        long DecrementBy(long value);

        void Set(long value);

        new DifferentialValueSample Current { get; }
    }
}
