using System;

namespace NDiagnostics.Metering.Counters
{
    internal sealed class ReadOnlyValueCounter : IValueCounter
    {
        #region Constants and Fields

        private readonly IValueCounter valueCounter;

        #endregion

        #region Constructors and Destructors

        internal ReadOnlyValueCounter(IValueCounter valueCounter)
        {
            this.valueCounter = valueCounter;
        }

        #endregion

        #region ICounter

        public string CategoryName => this.valueCounter.CategoryName;

        public string CounterName => this.valueCounter.CounterName;

        public string InstanceName => this.valueCounter.InstanceName;

        public InstanceLifetime InstanceLifetime => this.valueCounter.InstanceLifetime;

        public bool IsReadOnly => true;

        public long RawValue
        {
            get => this.valueCounter.RawValue;
            set => throw new InvalidOperationException("Cannot update ValueCounter, this object has been initialized as ReadOnly.");
        }

        public long Increment()
        {
            throw new InvalidOperationException("Cannot update ValueCounter, this object has been initialized as ReadOnly.");
        }

        public long IncrementBy(long value)
        {
            throw new InvalidOperationException("Cannot update ValueCounter, this object has been initialized as ReadOnly.");
        }

        public long Decrement()
        {
            throw new InvalidOperationException("Cannot update ValueCounter, this object has been initialized as ReadOnly.");
        }

        #endregion

        #region IValueCounter

        public IBaseCounter BaseCounter => this.valueCounter.BaseCounter.ReadOnly();

        public RawSample RawSample => this.valueCounter.RawSample;

        #endregion
    }
}
