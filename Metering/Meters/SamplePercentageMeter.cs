using NDiagnostics.Metering.Extensions;
using NDiagnostics.Metering.Samples;

namespace NDiagnostics.Metering.Meters
{
    internal sealed class SamplePercentageMeter : Meter, ISamplePercentage
    {
        #region Constructors and Destructors

        public SamplePercentageMeter(string categoryName, MeterCategoryType categoryType, string meterName, MeterType meterType, string instanceName, InstanceLifetime instanceLifetime, bool isReadOnly)
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
            this.ValueCounter.RawValue = 0;
            this.BaseCounter.RawValue = 0;
        }

        #endregion

        #region ISamplePercentage

        SamplePercentageSample ISamplePercentage.Current
        {
            get { return this.GetCurrentSample(); }
        }

        public void SampleA()
        {
            this.ThrowIfDisposed();
            this.ValueCounter.Increment();
            this.BaseCounter.Increment();
        }

        public void SampleB()
        {
            this.ThrowIfDisposed();
            this.BaseCounter.Increment();
        }

        #endregion

        #region Methods

        private SamplePercentageSample GetCurrentSample()
        {
            var sample = this.ValueCounter.RawSample;
            return new SamplePercentageSample(sample.Value, sample.BaseValue, sample.TimeStamp);
        }

        #endregion
    }
}
