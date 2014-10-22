using System.Diagnostics;
using NDiagnostics.Metering.Extensions;

namespace NDiagnostics.Metering.Counters
{
    internal sealed class SystemBaseCounter : SystemCounter, IBaseCounter
    {
        #region Constants and Fields

        private readonly PerformanceCounter performanceCounter;

        #endregion

        #region Constructors and Destructors

        internal SystemBaseCounter(string categoryName, string counterName, string instanceName, InstanceLifetime instanceLifetime, bool isReadOnly)
            : base(categoryName, counterName, instanceName, instanceLifetime, isReadOnly)
        {
            this.performanceCounter = new PerformanceCounter(categoryName, counterName, instanceName, isReadOnly);
        }

        #endregion

        #region ICounter

        public override long RawValue
        {
            get { return this.performanceCounter.RawValue; }
            set { this.performanceCounter.RawValue = value; }
        }

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
