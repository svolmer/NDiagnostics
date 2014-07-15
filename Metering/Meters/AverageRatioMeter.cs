using NDiagnostics.Metering.Samples;

namespace NDiagnostics.Metering.Meters
{
    internal sealed class AverageRatioMeter : Meter, IAverageRatio
    {
        #region Constructors and Destructors

        public AverageRatioMeter(string categoryName, MeterCategoryType categoryType, string meterName, MeterType meterType, string instanceName = null)
            : base(categoryName, categoryType, meterName, meterType, instanceName, true)
        {
            this.Reset();
        }

        #endregion

        #region IAverageRatio

        AverageRatioSample IAverageRatio.Current
        {
            get { return this.GetCurrentSample(); }
        }

        public void SampleSuccess()
        {
            this.ValueCounter.Increment();
            this.BaseCounter.Increment();
        }

        public void SampleFailure()
        {
            this.BaseCounter.Increment();
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
            this.BaseCounter.RawValue = 0;
        }

        #endregion

        #region Methods

        private AverageRatioSample GetCurrentSample()
        {
            var sample = this.ValueCounter.RawSample;
            return new AverageRatioSample(sample.Value, sample.BaseValue, sample.TimeStamp);
        }

        #endregion
    }
}
