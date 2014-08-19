namespace NDiagnostics.Metering.Samples
{
    public static class SampleExtensions
    {
        #region Methods

        public static long Value(this InstantValueSample sample)
        {
            return Sample.ComputeValue(sample);
        }

        public static float Value(this InstantPercentageSample sample)
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
