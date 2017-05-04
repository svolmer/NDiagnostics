using System;

namespace NDiagnostics.Metering.Counters
{
    internal sealed class ReadOnlyBaseCounter : IBaseCounter
    {
        #region Constants and Fields

        private readonly IBaseCounter baseCounter;

        #endregion

        #region Constructors and Destructors

        internal ReadOnlyBaseCounter(IBaseCounter baseCounter)
        {
            this.baseCounter = baseCounter;
        }

        #endregion

        #region ICounter

        public string CategoryName => this.baseCounter.CategoryName;

        public string CounterName => this.baseCounter.CounterName;

        public string InstanceName => this.baseCounter.InstanceName;

        public InstanceLifetime InstanceLifetime => this.baseCounter.InstanceLifetime;

        public bool IsReadOnly => true;

        public long RawValue
        {
            get => this.baseCounter.RawValue;
            set => throw new InvalidOperationException("Cannot update BaseCounter, this object has been initialized as ReadOnly.");
        }

        public long Increment()
        {
            throw new InvalidOperationException("Cannot update BaseCounter, this object has been initialized as ReadOnly.");
        }

        public long IncrementBy(long value)
        {
            throw new InvalidOperationException("Cannot update BaseCounter, this object has been initialized as ReadOnly.");
        }

        public long Decrement()
        {
            throw new InvalidOperationException("Cannot update BaseCounter, this object has been initialized as ReadOnly.");
        }

        #endregion
    }
}
