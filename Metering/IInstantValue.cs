using NDiagnostics.Metering.Samples;

namespace NDiagnostics.Metering
{
    public interface IInstantValue : IMeter<InstantValueSample>
    {
        long Increment();

        long IncrementBy(long value);

        long Decrement();

        long DecrementBy(long value);

        void Set(long value);
    }
}
