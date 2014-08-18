using NDiagnostics.Metering.Extensions;
using NDiagnostics.Metering.Samples;

namespace NDiagnostics.Metering.Meters
{
    internal sealed class InstantPercentageMeter : Meter, IInstantPercentage
    {
        #region Constructors and Destructors

        public InstantPercentageMeter(string categoryName, MeterCategoryType categoryType, string meterName, MeterType meterType, string instanceName, MeterInstanceLifetime instanceLifetime = MeterInstanceLifetime.Global)
            : base(categoryName, categoryType, meterName, meterType, instanceName, instanceLifetime, true)
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
            this.ThrowIfDisposed();
            return this.ValueCounter.Increment();
        }

        public long IncrementNumeratorBy(long value)
        {
            this.ThrowIfDisposed();
            return this.ValueCounter.IncrementBy(value);
        }

        public long DecrementNumerator()
        {
            this.ThrowIfDisposed();
            return this.ValueCounter.Decrement();
        }

        public long DecrementNumeratorBy(long value)
        {
            this.ThrowIfDisposed();
            return this.ValueCounter.IncrementBy(-value);
        }

        public void SetNumerator(long value)
        {
            this.ThrowIfDisposed();
            this.ValueCounter.RawValue = value;
        }

        public long IncrementDenominator()
        {
            this.ThrowIfDisposed();
            return this.BaseCounter.Increment();
        }

        public long IncrementDenominatorBy(long value)
        {
            this.ThrowIfDisposed();
            return this.BaseCounter.IncrementBy(value);
        }

        public long DecrementDenominator()
        {
            this.ThrowIfDisposed();
            return this.BaseCounter.Decrement();
        }

        public long DecrementDenominatorBy(long value)
        {
            this.ThrowIfDisposed();
            return this.BaseCounter.IncrementBy(-value);
        }

        public void SetDenominator(long value)
        {
            this.ThrowIfDisposed();
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
            this.ThrowIfDisposed();
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
