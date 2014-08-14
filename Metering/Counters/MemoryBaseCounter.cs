﻿using System.Threading;
using NDiagnostics.Metering.Extensions;

namespace NDiagnostics.Metering.Counters
{
    internal sealed class MemoryBaseCounter : BaseCounter
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

        #region Properties

        public override long RawValue
        {
            get { return Interlocked.Read(ref this.n); }
            set { Interlocked.Exchange(ref this.n, value); }
        }

        #endregion

        #region Public Methods

        public override long Increment()
        {
            this.ThrowIfDisposed();
            return Interlocked.Increment(ref this.n);
        }

        public override long IncrementBy(long value)
        {
            this.ThrowIfDisposed();
            return Interlocked.Add(ref this.n, value);
        }

        public override long Decrement()
        {
            this.ThrowIfDisposed();
            return Interlocked.Decrement(ref this.n);
        }

        #endregion

        #region Methods

        protected override void OnDisposing()
        {
        }

        #endregion
    }
}
