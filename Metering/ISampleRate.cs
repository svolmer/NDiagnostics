using NDiagnostics.Metering.Samples;

namespace NDiagnostics.Metering
{
    public interface ISampleRate : IMeter
    {
        void Sample();

        new SampleRateSample Current { get; }
    }
}
