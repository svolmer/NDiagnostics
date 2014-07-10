using NDiagnostics.Metering.Samples;
using NDiagnostics.Metering.Types;

namespace NDiagnostics.Metering.Meters
{
    internal sealed class Timer100nsMeter : Meter, ITimer100Ns
    {
        #region Constructors and Destructors

        public Timer100nsMeter(string categoryName, MeterCategoryType categoryType, string meterName, MeterType meterType, string instanceName = null)
            : base(categoryName, categoryType, meterName, meterType, instanceName, false)
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
            this.ValueCounter.RawValue = 0;
        }

        #endregion

        #region ITimer100Ns

        Timer100NsSample ITimer100Ns.Current
        {
            get { return this.GetCurrentSample(); }
        }

        public void Sample(Time100Ns elapsedTimeOfActivity)
        {
            this.ValueCounter.IncrementBy(elapsedTimeOfActivity.Ticks);
        }

        #endregion

        #region Methods

        private Timer100NsSample GetCurrentSample()
        {
            var sample = this.ValueCounter.RawSample;
            return new Timer100NsSample(new Time100Ns(sample.Value), sample.TimeStamp100Ns);
        }

        #endregion
    }
}
