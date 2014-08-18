using NDiagnostics.Metering.Extensions;
using NDiagnostics.Metering.Samples;
using NDiagnostics.Metering.Types;

namespace NDiagnostics.Metering.Meters
{
    internal sealed class InstantTimeMeter : Meter, IInstantTime
    {
        #region Constructors and Destructors

        public InstantTimeMeter(string categoryName, MeterCategoryType categoryType, string meterName, MeterType meterType, string instanceName, MeterInstanceLifetime instanceLifetime = MeterInstanceLifetime.Global)
            : base(categoryName, categoryType, meterName, meterType, instanceName, instanceLifetime, false)
        {
            this.Reset();
        }

        #endregion

        #region IInstantTime

        InstantTimeSample IInstantTime.Current
        {
            get { return this.GetCurrentSample(); }
        }

        public void Set(TimeStamp start)
        {
            this.ThrowIfDisposed();
            this.ValueCounter.RawValue = start.Ticks;
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
            this.ValueCounter.RawValue = TimeStamp.Now.Ticks;
        }

        #endregion

        #region Methods

        private InstantTimeSample GetCurrentSample()
        {
            var sample = this.ValueCounter.RawSample;
            return new InstantTimeSample(new TimeStamp(sample.Value), sample.TimeStamp);
        }

        #endregion
    }
}
