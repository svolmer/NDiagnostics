using System.Threading;

namespace NDiagnostics.Metering.Counters
{
    internal sealed class MemoryBaseCounter : MemoryCounter, IBaseCounter
    {
        #region Constants and Fields

        private long n;

        #endregion

        #region Constructors and Destructors

        internal MemoryBaseCounter(string categoryName, string counterName, string instanceName)
            : base(categoryName, counterName, instanceName)
        {
            Interlocked.Exchange(ref this.n, 0L);
        }

        #endregion

        #region ICounter

        public override long RawValue
        {
            get { return Interlocked.Read(ref this.n); }
            set { Interlocked.Exchange(ref this.n, value); }
        }

        public override long Increment()
        {
            return Interlocked.Increment(ref this.n);
        }

        public override long IncrementBy(long value)
        {
            return Interlocked.Add(ref this.n, value);
        }

        public override long Decrement()
        {
            return Interlocked.Decrement(ref this.n);
        }

        #endregion
    }
}
