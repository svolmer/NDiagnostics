using NDiagnostics.Metering.Extensions;
using NDiagnostics.Metering.Samples;

namespace NDiagnostics.Metering.Meters
{
    internal sealed class AverageValueMeter : Meter<AverageValueSample>, IAverageValue
    {
        #region Constructors and Destructors

        public AverageValueMeter(string categoryName, MeterCategoryType categoryType, string meterName, MeterType meterType, string instanceName, InstanceLifetime instanceLifetime, bool isReadOnly)
            : base(categoryName, categoryType, meterName, meterType, instanceName, instanceLifetime, isReadOnly, true)
        {
        }

        #endregion

        #region IAverageValue

        public void Sample(long value)
        {
            this.ThrowIfDisposed();
            this.ValueCounter.IncrementBy(value);
            this.BaseCounter.Increment();
        }

        #endregion

        #region IMeter

        public override AverageValueSample Current => this.GetCurrentSample();

        public override void Reset()
        {
            this.ThrowIfDisposed();
            this.ValueCounter.RawValue = 0;
            this.BaseCounter.RawValue = 0;
        }

        #endregion

        #region Methods

        private AverageValueSample GetCurrentSample()
        {
            var sample = this.ValueCounter.RawSample;
            return new AverageValueSample(sample.Value, sample.BaseValue, sample.TimeStamp, sample.TimeStamp100Ns);
        }

        #endregion
    }
}
