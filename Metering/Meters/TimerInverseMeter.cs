using NDiagnostics.Metering.Extensions;
using NDiagnostics.Metering.Samples;
using NDiagnostics.Metering.Types;

namespace NDiagnostics.Metering.Meters
{
    internal sealed class TimerInverseMeter : Meter<TimerInverseSample>, ITimerInverse
    {
        #region Constructors and Destructors

        public TimerInverseMeter(string categoryName, MeterCategoryType categoryType, string meterName, MeterType meterType, string instanceName, InstanceLifetime instanceLifetime, bool isReadOnly)
            : base(categoryName, categoryType, meterName, meterType, instanceName, instanceLifetime, isReadOnly, false)
        {
        }

        #endregion

        #region IMeter

        public override TimerInverseSample Current => this.GetCurrentSample();

        public override void Reset()
        {
            this.ThrowIfDisposed();
            this.ValueCounter.RawValue = 0L;
        }

        #endregion

        #region ITimerInverse

        public void Sample(Time time)
        {
            this.ThrowIfDisposed();
            this.ValueCounter.IncrementBy(time.Ticks);
        }

        #endregion

        #region Methods

        private TimerInverseSample GetCurrentSample()
        {
            var sample = this.ValueCounter.RawSample;
            return new TimerInverseSample(new Time(sample.Value), sample.TimeStamp, sample.TimeStamp100Ns);
        }

        #endregion
    }
}
