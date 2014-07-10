﻿using NDiagnostics.Metering.Samples;
using NDiagnostics.Metering.Types;

namespace NDiagnostics.Metering.Meters
{
    internal sealed class MultiTimerInverseMeter : Meter, IMultiTimerInverse
    {
        #region Constructors and Destructors

        public MultiTimerInverseMeter(string categoryName, MeterCategoryType categoryType, string meterName, MeterType meterType, string instanceName = null)
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
            this.ValueCounter.RawValue = 0L;
            this.BaseCounter.RawValue = 0L;
        }

        #endregion

        #region IMultiTimerInverse

        MultiTimerInverseSample IMultiTimerInverse.Current
        {
            get { return this.GetCurrentSample(); }
        }

        public void Sample(Time time)
        {
            this.ValueCounter.IncrementBy(time.Ticks);
            this.BaseCounter.Increment();
        }

        #endregion

        #region Methods

        private MultiTimerInverseSample GetCurrentSample()
        {
            var sample = this.ValueCounter.RawSample;
            return new MultiTimerInverseSample(new Time(sample.Value), sample.BaseValue, sample.TimeStamp);
        }

        #endregion
    }
}
