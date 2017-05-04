using NDiagnostics.Metering.Samples;

namespace NDiagnostics.Metering
{
    public interface ISampleRate : IMeter<SampleRateSample>
    {
        void Sample();
    }
}
