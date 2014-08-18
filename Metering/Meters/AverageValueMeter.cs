using NDiagnostics.Metering.Extensions;
using NDiagnostics.Metering.Samples;

namespace NDiagnostics.Metering.Meters
{
    internal sealed class AverageValueMeter : Meter, IAverageValue
    {
        #region Constructors and Destructors

        public AverageValueMeter(string categoryName, MeterCategoryType categoryType, string meterName, MeterType meterType, string instanceName)
            : base(categoryName, categoryType, meterName, meterType, instanceName, true)
        {
            this.Reset();
        }

        #endregion

        #region IAverageValue

        AverageValueSample IAverageValue.Current
        {
            get { return this.GetCurrentSample(); }
        }

        public void Sample(long value)
        {
            this.ThrowIfDisposed();
            this.ValueCounter.IncrementBy(value);
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
            this.ThrowIfDisposed();
            this.ValueCounter.RawValue = 0;
            this.BaseCounter.RawValue = 0;
        }

        #endregion

        #region Methods

        private AverageValueSample GetCurrentSample()
        {
            var sample = this.ValueCounter.RawSample;
            return new AverageValueSample(sample.Value, sample.BaseValue, sample.TimeStamp);
        }

        #endregion
    }
}
