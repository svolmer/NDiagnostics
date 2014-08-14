using NDiagnostics.Metering.Extensions;
using NDiagnostics.Metering.Samples;
using NDiagnostics.Metering.Types;

namespace NDiagnostics.Metering.Meters
{
    internal sealed class MultiTimer100NsInverseMeter : Meter, IMultiTimer100NsInverse
    {
        #region Constructors and Destructors

        public MultiTimer100NsInverseMeter(string categoryName, MeterCategoryType categoryType, string meterName, MeterType meterType, string instanceName = null)
            : base(categoryName, categoryType, meterName, meterType, instanceName, true)
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

        #region IMultiTimer100NsInverse

        MultiTimer100NsInverseSample IMultiTimer100NsInverse.Current
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

        private MultiTimer100NsInverseSample GetCurrentSample()
        {
            var sample = this.ValueCounter.RawSample;
            return new MultiTimer100NsInverseSample(new Time(sample.Value), sample.BaseValue, sample.TimeStamp);
        }

        #endregion
    }
}
