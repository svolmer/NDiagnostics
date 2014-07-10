using NDiagnostics.Metering.Samples;

namespace NDiagnostics.Metering.Extensions
{
    public static class SampleExtensions
    {
        #region Public Methods

        public static long Value(this InstantValueSample sample)
        {
            return Sample.ComputeValue(sample);
        }

        public static float Value(this InstantRatioSample sample)
        {
            return Sample.ComputeValue(sample);
        }

        public static float Value(this InstantTimeSample sample)
        {
            return Sample.ComputeValue(sample);
        }

        #endregion
    }
}
