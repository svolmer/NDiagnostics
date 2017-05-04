using NDiagnostics.Metering.Extensions;
using NDiagnostics.Metering.Samples;
using NDiagnostics.Metering.Types;

namespace NDiagnostics.Metering.Meters
{
    internal sealed class Timer100nsMeter : Meter<Timer100NsSample>, ITimer100Ns
    {
        #region Constructors and Destructors

        public Timer100nsMeter(string categoryName, MeterCategoryType categoryType, string meterName, MeterType meterType, string instanceName, InstanceLifetime instanceLifetime, bool isReadOnly)
            : base(categoryName, categoryType, meterName, meterType, instanceName, instanceLifetime, isReadOnly, false)
        {
        }

        #endregion

        #region IMeter

        public override Timer100NsSample Current => this.GetCurrentSample();

        public override void Reset()
        {
            this.ThrowIfDisposed();
            this.ValueCounter.RawValue = 0;
        }

        #endregion

        #region ITimer100Ns

        public void Sample(Time100Ns elapsedTimeOfActivity)
        {
            this.ThrowIfDisposed();
            this.ValueCounter.IncrementBy(elapsedTimeOfActivity.Ticks);
        }

        #endregion

        #region Methods

        private Timer100NsSample GetCurrentSample()
        {
            var sample = this.ValueCounter.RawSample;
            return new Timer100NsSample(new Time100Ns(sample.Value), sample.TimeStamp, sample.TimeStamp100Ns);
        }

        #endregion
    }
}
