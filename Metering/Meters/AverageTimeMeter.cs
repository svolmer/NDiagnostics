using NDiagnostics.Metering.Extensions;
using NDiagnostics.Metering.Samples;
using NDiagnostics.Metering.Types;

namespace NDiagnostics.Metering.Meters
{
    internal sealed class AverageTimeMeter : Meter, IAverageTime
    {
        #region Constructors and Destructors

        public AverageTimeMeter(string categoryName, MeterCategoryType categoryType, string meterName, MeterType meterType, string instanceName, InstanceLifetime instanceLifetime)
            : base(categoryName, categoryType, meterName, meterType, instanceName, instanceLifetime, true)
        {
            this.Reset();
        }

        #endregion

        #region IAverageTime

        AverageTimeSample IAverageTime.Current
        {
            get { return this.GetCurrentSample(); }
        }

        public void Sample(Time elapsedTime)
        {
            this.ThrowIfDisposed();
            this.ValueCounter.IncrementBy(elapsedTime.Ticks);
            this.BaseCounter.Increment();
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

        #region Methods

        private AverageTimeSample GetCurrentSample()
        {
            var sample = this.ValueCounter.RawSample;
            return new AverageTimeSample(new Time(sample.Value), sample.BaseValue, sample.TimeStamp);
        }

        #endregion
    }
}
