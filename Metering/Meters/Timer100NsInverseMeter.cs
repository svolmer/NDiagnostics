using NDiagnostics.Metering.Extensions;
using NDiagnostics.Metering.Samples;
using NDiagnostics.Metering.Types;

namespace NDiagnostics.Metering.Meters
{
    internal sealed class Timer100nsInverseMeter : Meter, ITimer100NsInverse
    {
        #region Constructors and Destructors

        public Timer100nsInverseMeter(string categoryName, MeterCategoryType categoryType, string meterName, MeterType meterType, string instanceName, InstanceLifetime instanceLifetime, bool isReadOnly)
            : base(categoryName, categoryType, meterName, meterType, instanceName, instanceLifetime, isReadOnly, false)
        {
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

        #region ITimer100NsInverse

        Timer100NsInverseSample ITimer100NsInverse.Current
        {
            get { return this.GetCurrentSample(); }
        }

        public void Sample(Time100Ns elapsedTimeOfInactivity)
        {
            this.ThrowIfDisposed();
            this.ValueCounter.IncrementBy(elapsedTimeOfInactivity.Ticks);
        }

        #endregion

        #region Methods

        private Timer100NsInverseSample GetCurrentSample()
        {
            var sample = this.ValueCounter.RawSample;
            return new Timer100NsInverseSample(new Time100Ns(sample.Value), sample.TimeStamp100Ns);
        }

        #endregion
    }
}
