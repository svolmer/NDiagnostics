using NDiagnostics.Metering.Samples;

namespace NDiagnostics.Metering
{
    public interface IInstantValue : IMeter
    {
        long Increment();

        long IncrementBy(long value);

        long Decrement();

        long DecrementBy(long value);

        void Set(long value);

        new InstantValueSample Current { get; }
    }
}
