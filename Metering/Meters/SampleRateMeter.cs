using NDiagnostics.Metering.Extensions;
using NDiagnostics.Metering.Samples;

namespace NDiagnostics.Metering.Meters
{
    internal sealed class SampleRateMeter : Meter, ISampleRate
    {
        #region Constructors and Destructors

        public SampleRateMeter(string categoryName, MeterCategoryType categoryType, string meterName, MeterType meterType, string instanceName, InstanceLifetime instanceLifetime = Metering.InstanceLifetime.Global)
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
            this.ValueCounter.RawValue = 0;
        }

        #endregion

        #region ISampleRate

        SampleRateSample ISampleRate.Current
        {
            get { return this.GetCurrentSample(); }
        }

        public void Sample()
        {
            this.ThrowIfDisposed();
            this.ValueCounter.Increment();
        }

        #endregion

        #region Methods

        private SampleRateSample GetCurrentSample()
        {
            var sample = this.ValueCounter.RawSample;
            return new SampleRateSample(sample.Value, sample.TimeStamp);
        }

        #endregion
    }
}
