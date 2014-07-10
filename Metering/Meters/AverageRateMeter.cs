using NDiagnostics.Metering.Samples;

namespace NDiagnostics.Metering.Meters
{
    internal sealed class AverageRateMeter : Meter, IAverageRate
    {
        #region Constructors and Destructors

        public AverageRateMeter(string categoryName, MeterCategoryType categoryType, string meterName, MeterType meterType, string instanceName = null)
            : base(categoryName, categoryType, meterName, meterType, instanceName, false)
        {
            this.Reset();
        }

        #endregion

        #region IAverageRate

        AverageRateSample IAverageRate.Current
        {
            get { return this.GetCurrentSample(); }
        }

        public void Sample()
        {
            this.ValueCounter.Increment();
        }

        #endregion

        #region IMeter

        public override Sample Current
        {
            get { return this.GetCurrentSample(); }
        }

        public override void Reset()
        {
            this.ValueCounter.RawValue = 0;
        }

        #endregion

        #region Methods

        private AverageRateSample GetCurrentSample()
        {
            var sample = this.ValueCounter.RawSample;
            return new AverageRateSample(sample.Value, sample.TimeStamp);
        }

        #endregion
    }
}
