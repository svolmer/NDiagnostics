using NDiagnostics.Metering.Samples;

namespace NDiagnostics.Metering.Meters
{
    internal sealed class InstantPercentageMeter : Meter, IInstantPercentage
    {
        #region Constructors and Destructors

        public InstantPercentageMeter(string categoryName, MeterCategoryType categoryType, string meterName, MeterType meterType, string instanceName = null)
            : base(categoryName, categoryType, meterName, meterType, instanceName, true)
        {
            this.Reset();
        }

        #endregion

        #region IInstantPercentage

        InstantPercentageSample IInstantPercentage.Current
        {
            get { return this.GetCurrentSample(); }
        }

        public long IncrementNumerator()
        {
            return this.ValueCounter.Increment();
        }

        public long IncrementNumeratorBy(long value)
        {
            return this.ValueCounter.IncrementBy(value);
        }

        public long DecrementNumerator()
        {
            return this.ValueCounter.Decrement();
        }

        public long DecrementNumeratorBy(long value)
        {
            return this.ValueCounter.IncrementBy(-value);
        }

        public void SetNumerator(long value)
        {
            this.ValueCounter.RawValue = value;
        }

        public long IncrementDenominator()
        {
            return this.BaseCounter.Increment();
        }

        public long IncrementDenominatorBy(long value)
        {
            return this.BaseCounter.IncrementBy(value);
        }

        public long DecrementDenominator()
        {
            return this.BaseCounter.Decrement();
        }

        public long DecrementDenominatorBy(long value)
        {
            return this.BaseCounter.IncrementBy(-value);
        }

        public void SetDenominator(long value)
        {
            this.BaseCounter.RawValue = value;
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

        private InstantPercentageSample GetCurrentSample()
        {
            var sample = this.ValueCounter.RawSample;
            return new InstantPercentageSample(sample.Value, sample.BaseValue, sample.TimeStamp);
        }

        #endregion
    }
}
