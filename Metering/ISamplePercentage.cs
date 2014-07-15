using NDiagnostics.Metering.Samples;

namespace NDiagnostics.Metering
{
    public interface ISamplePercentage : IMeter
    {
        void SampleSuccess();

        void SampleFailure();

        new SamplePercentageSample Current { get; }
    }
}
