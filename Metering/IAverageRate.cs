using NDiagnostics.Metering.Samples;

namespace NDiagnostics.Metering
{
    public interface IAverageRate : IMeter
    {
        void Sample();

        new AverageRateSample Current { get; }
    }
}
