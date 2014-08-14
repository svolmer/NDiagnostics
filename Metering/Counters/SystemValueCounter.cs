using System.Diagnostics;
using NDiagnostics.Metering.Extensions;
using NDiagnostics.Metering.Types;

namespace NDiagnostics.Metering.Counters
{
    internal sealed class SystemValueCounter : ValueCounter
    {
        #region Constants and Fields

        private readonly PerformanceCounter performanceCounter;

        #endregion

        #region Constructors and Destructors

        internal SystemValueCounter(string categoryName, string counterName, string instanceName, BaseCounter baseCounter)
            : base(categoryName, counterName, instanceName, baseCounter)
        {
            this.performanceCounter = new PerformanceCounter(categoryName, counterName, instanceName, false);
        }

        #endregion

        #region Properties

        public override long RawValue
        {
            get { return this.performanceCounter.RawValue; }
            set { this.performanceCounter.RawValue = value; }
        }

        public override RawSample RawSample
        {
            get
            {
                var sample = this.performanceCounter.NextSample();
                return new RawSample(sample.RawValue, sample.BaseValue, new TimeStamp(sample.TimeStamp), new TimeStamp100Ns(sample.TimeStamp100nSec));
            }
        }

        #endregion

        #region Public Methods

        public override long Increment()
        {
            this.ThrowIfDisposed();
            return this.performanceCounter.Increment();
        }

        public override long IncrementBy(long n)
        {
            this.ThrowIfDisposed();
            return this.performanceCounter.IncrementBy(n);
        }

        public override long Decrement()
        {
            this.ThrowIfDisposed();
            return this.performanceCounter.Decrement();
        }

        #endregion

        #region Methods

        protected override void OnDisposing()
        {
            this.performanceCounter.Dispose();
        }

        #endregion
    }
}
