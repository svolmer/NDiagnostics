using NDiagnostics.Metering.Extensions;
using NDiagnostics.Metering.Samples;

namespace NDiagnostics.Metering.Meters
{
    internal sealed class DifferentialValueMeter : Meter, IDifferentialValue
    {
        #region Constructors and Destructors

        public DifferentialValueMeter(string categoryName, MeterCategoryType categoryType, string meterName, MeterType meterType, string instanceName, InstanceLifetime instanceLifetime)
            : base(categoryName, categoryType, meterName, meterType, instanceName, instanceLifetime, false)
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
            this.ThrowIfDisposed();
            return this.ValueCounter.Increment();
        }

        public long IncrementBy(long value)
        {
            this.ThrowIfDisposed();
            return this.ValueCounter.IncrementBy(value);
        }

        public long Decrement()
        {
            this.ThrowIfDisposed();
            return this.ValueCounter.Decrement();
        }

        public long DecrementBy(long value)
        {
            this.ThrowIfDisposed();
            return this.ValueCounter.IncrementBy(-value);
        }

        public void Set(long value)
        {
            this.ThrowIfDisposed();
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
            this.ThrowIfDisposed();
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
