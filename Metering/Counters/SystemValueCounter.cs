﻿using System.Diagnostics;
using NDiagnostics.Metering.Extensions;
using NDiagnostics.Metering.Types;

namespace NDiagnostics.Metering.Counters
{
    internal sealed class SystemValueCounter : SystemCounter, IValueCounter
    {
        #region Constants and Fields

        private readonly PerformanceCounter performanceCounter;

        #endregion

        #region Constructors and Destructors

        internal SystemValueCounter(string categoryName, string counterName, string instanceName, InstanceLifetime instanceLifetime, bool isReadOnly, IBaseCounter baseCounter)
            : base(categoryName, counterName, instanceName, instanceLifetime, isReadOnly)
        {
            this.BaseCounter = baseCounter;
            this.performanceCounter = new PerformanceCounter(categoryName, counterName, instanceName, isReadOnly);
        }

        #endregion

        #region ICounter

        public override long RawValue
        {
            get => this.performanceCounter.RawValue;
            set => this.performanceCounter.RawValue = value;
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

        #region IValueCounter

        public IBaseCounter BaseCounter { get; }

        public RawSample RawSample
        {
            get
            {
                var sample = this.performanceCounter.NextSample();
                return new RawSample(sample.RawValue, sample.BaseValue, new TimeStamp(sample.TimeStamp), new TimeStamp100Ns(sample.TimeStamp100nSec));
            }
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
