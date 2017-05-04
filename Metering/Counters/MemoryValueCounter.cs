using System;
using System.Threading;
using NDiagnostics.Metering.Types;

namespace NDiagnostics.Metering.Counters
{
    internal sealed class MemoryValueCounter : MemoryCounter, IValueCounter
    {
        #region Constants and Fields

        private long n;

        #endregion

        #region Constructors and Destructors

        internal MemoryValueCounter(string categoryName, string counterName, string instanceName, InstanceLifetime instanceLifetime, IBaseCounter baseCounter)
            : base(categoryName, counterName, instanceName, instanceLifetime)
        {
            this.BaseCounter = baseCounter;
            Interlocked.Exchange(ref this.n, 0L);
        }

        #endregion

        #region ICounter

        public override long RawValue
        {
            get => Interlocked.Read(ref this.n);
            set => Interlocked.Exchange(ref this.n, value);
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

        #region IValueCounter

        public IBaseCounter BaseCounter { get; }

        public RawSample RawSample
        {
            get
            {
                var rawValue = this.RawValue;
                var baseValue = BaseCounter?.RawValue ?? 0L;
                var timeStamp = TimeStamp.Now;
                var timeStamp100Ns = TimeStamp100Ns.Now;
                return new RawSample(rawValue, baseValue, timeStamp, timeStamp100Ns);
            }
        }

        #endregion
    }
}
