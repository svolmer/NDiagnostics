using NDiagnostics.Metering.Extensions;
using NDiagnostics.Metering.Samples;
using NDiagnostics.Metering.Types;

namespace NDiagnostics.Metering.Meters
{
    internal sealed class MultiTimerMeter : Meter, IMultiTimer
    {
        #region Constructors and Destructors

        public MultiTimerMeter(string categoryName, MeterCategoryType categoryType, string meterName, MeterType meterType, string instanceName, InstanceLifetime instanceLifetime)
            : base(categoryName, categoryType, meterName, meterType, instanceName, instanceLifetime, true)
        {
            this.Reset();
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
            this.ValueCounter.RawValue = 0L;
            this.BaseCounter.RawValue = 0L;
        }

        #endregion

        #region IMultiTimer

        MultiTimerSample IMultiTimer.Current
        {
            get { return this.GetCurrentSample(); }
        }

        public void Sample(Time time)
        {
            this.ThrowIfDisposed();
            this.ValueCounter.IncrementBy(time.Ticks);
            this.BaseCounter.Increment();
        }

        #endregion

        #region Methods

        private MultiTimerSample GetCurrentSample()
        {
            var sample = this.ValueCounter.RawSample;
            return new MultiTimerSample(new Time(sample.Value), sample.BaseValue, sample.TimeStamp);
        }

        #endregion
    }
}
