using System.Threading;
using NDiagnostics.Metering.Types;

namespace NDiagnostics.Metering.Counters
{
    internal sealed class MemoryValueCounter : ValueCounter
    {
        #region Constants and Fields

        private long n;

        #endregion

        #region Constructors and Destructors

        internal MemoryValueCounter(string categoryName, string counterName, string instanceName, BaseCounter baseCounter)
            : base(categoryName, counterName, instanceName, baseCounter)
        {
            Interlocked.Exchange(ref this.n, 0L);
        }

        #endregion

        #region Properties

        public override long RawValue
        {
            get { return Interlocked.Read(ref this.n); }
            set { Interlocked.Exchange(ref this.n, value); }
        }

        public override RawSample RawSample
        {
            get
            {
                var rawValue = this.RawValue;
                var baseValue = (this.BaseCounter != null) ? this.BaseCounter.RawValue : 0L;
                var timeStamp = TimeStamp.Now;
                return new RawSample(rawValue, baseValue, timeStamp);
            }
        }

        #endregion

        #region Public Methods

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

        #region Methods

        protected override void InternalDispose()
        {
        }

        #endregion
    }
}
