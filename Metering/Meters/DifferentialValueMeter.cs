using NDiagnostics.Metering.Samples;

namespace NDiagnostics.Metering.Meters
{
    internal sealed class DifferentialValueMeter : Meter, IDifferentialValue
    {
        #region Constructors and Destructors

        public DifferentialValueMeter(string categoryName, MeterCategoryType categoryType, string meterName, MeterType meterType, string instanceName = null)
            : base(categoryName, categoryType, meterName, meterType, instanceName)
        {
            this.Reset();
        }

        #endregion

        #region IDifferentialValue

        DifferentialValueSample IDifferentialValue.Current
        {
            get { return this.GetCurrentSample(); }
        }

        public long Increment()
        {
            return this.ValueCounter.Increment();
        }

        public long IncrementBy(long value)
        {
            return this.ValueCounter.IncrementBy(value);
        }

        public long Decrement()
        {
            return this.ValueCounter.Decrement();
        }

        public long DecrementBy(long value)
        {
            return this.ValueCounter.IncrementBy(-value);
        }

        public void Set(long value)
        {
            this.ValueCounter.RawValue = value;
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

        private DifferentialValueSample GetCurrentSample()
        {
            var sample = this.ValueCounter.RawSample;
            return new DifferentialValueSample(sample.Value, sample.TimeStamp);
        }

        #endregion
    }
}
