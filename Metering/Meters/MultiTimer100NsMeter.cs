using NDiagnostics.Metering.Extensions;
using NDiagnostics.Metering.Samples;
using NDiagnostics.Metering.Types;

namespace NDiagnostics.Metering.Meters
{
    internal sealed class MultiTimer100NsMeter : Meter, IMultiTimer100Ns
    {
        #region Constructors and Destructors

        public MultiTimer100NsMeter(string categoryName, MeterCategoryType categoryType, string meterName, MeterType meterType, string instanceName, InstanceLifetime instanceLifetime, bool isReadOnly)
            : base(categoryName, categoryType, meterName, meterType, instanceName, instanceLifetime, isReadOnly, true)
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
            this.ValueCounter.RawValue = 0L;
            this.BaseCounter.RawValue = 0L;
        }

        #endregion

        #region IMultiTimer100Ns

        MultiTimer100NsSample IMultiTimer100Ns.Current
        {
            get { return this.GetCurrentSample(); }
        }

        public void Sample(Time100Ns time)
        {
            this.ThrowIfDisposed();
            this.ValueCounter.IncrementBy(time.Ticks);
            this.BaseCounter.Increment();
        }

        #endregion

        #region Methods

        private MultiTimer100NsSample GetCurrentSample()
        {
            var sample = this.ValueCounter.RawSample;
            return new MultiTimer100NsSample(new Time100Ns(sample.Value), sample.BaseValue, sample.TimeStamp, sample.TimeStamp100Ns);
        }

        #endregion
    }
}
