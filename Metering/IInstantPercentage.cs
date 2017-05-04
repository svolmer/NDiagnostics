using NDiagnostics.Metering.Samples;

namespace NDiagnostics.Metering
{
    public interface IInstantPercentage : IMeter<InstantPercentageSample>
    {
        long IncrementNumerator();

        long IncrementNumeratorBy(long value);

        long DecrementNumerator();

        long DecrementNumeratorBy(long value);

        void SetNumerator(long value);

        long IncrementDenominator();

        long IncrementDenominatorBy(long value);

        long DecrementDenominator();

        long DecrementDenominatorBy(long value);

        void SetDenominator(long value);
    }
}
