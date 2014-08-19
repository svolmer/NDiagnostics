using NDiagnostics.Metering.Extensions;
using NDiagnostics.Metering.Samples;
using NDiagnostics.Metering.Types;

namespace NDiagnostics.Metering.Meters
{
    internal sealed class TimerMeter : Meter, ITimer
    {
        #region Constructors and Destructors

        public TimerMeter(string categoryName, MeterCategoryType categoryType, string meterName, MeterType meterType, string instanceName, InstanceLifetime instanceLifetime)
            : base(categoryName, categoryType, meterName, meterType, instanceName, instanceLifetime, false)
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
        }

        #endregion

        #region ITimer

        TimerSample ITimer.Current
        {
            get { return this.GetCurrentSample(); }
        }

        public void Sample(Time time)
        {
            this.ThrowIfDisposed();
            this.ValueCounter.IncrementBy(time.Ticks);
        }

        #endregion

        #region Methods

        private TimerSample GetCurrentSample()
        {
            var sample = this.ValueCounter.RawSample;
            return new TimerSample(new Time(sample.Value), sample.TimeStamp);
        }

        #endregion
    }
}
