using NDiagnostics.Metering.Extensions;
using NDiagnostics.Metering.Samples;

namespace NDiagnostics.Metering.Meters
{
    internal sealed class InstantValueMeter : Meter, IInstantValue
    {
        #region Constructors and Destructors

        public InstantValueMeter(string categoryName, MeterCategoryType categoryType, string meterName, MeterType meterType, string instanceName, InstanceLifetime instanceLifetime)
            : base(categoryName, categoryType, meterName, meterType, instanceName, instanceLifetime, false)
        {
            this.Reset();
        }

        #endregion

        #region IInstantValue

        InstantValueSample IInstantValue.Current
        {
            get { return this.GetCurrentSample(); }
        }

        public void Set(long value)
        {
            this.ThrowIfDisposed();
            this.ValueCounter.RawValue = value;
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

        private InstantValueSample GetCurrentSample()
        {
            var sample = this.ValueCounter.RawSample;
            return new InstantValueSample(sample.Value, sample.TimeStamp);
        }

        #endregion
    }
}
