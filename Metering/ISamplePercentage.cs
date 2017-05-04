using NDiagnostics.Metering.Samples;

namespace NDiagnostics.Metering
{
    public interface ISamplePercentage : IMeter<SamplePercentageSample>
    {
        void SampleA();

        void SampleB();
    }
}
