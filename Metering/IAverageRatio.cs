using NDiagnostics.Metering.Samples;

namespace NDiagnostics.Metering
{
    public interface IAverageRatio : IMeter
    {
        void SampleSuccess();

        void SampleFailure();

        new AverageRatioSample Current { get; }
    }
}
