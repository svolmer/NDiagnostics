using NDiagnostics.Metering.Samples;

namespace NDiagnostics.Metering
{
    public interface ISamplePercentage : IMeter
    {
        void SampleA();

        void SampleB();

        new SamplePercentageSample Current { get; }
    }
}
