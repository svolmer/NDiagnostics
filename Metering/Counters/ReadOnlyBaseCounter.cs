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

        public string CategoryName
        {
            get { return this.baseCounter.CategoryName; }
        }

        public string CounterName
        {
            get { return this.baseCounter.CounterName; }
        }

        public string InstanceName
        {
            get { return this.baseCounter.InstanceName; }
        }

        public InstanceLifetime InstanceLifetime
        {
            get { return this.baseCounter.InstanceLifetime; }
        }

        public bool IsReadOnly
        {
            get { return true; }
        }

        public long RawValue
        {
            get { return this.baseCounter.RawValue; }
            set { throw new InvalidOperationException("Cannot update BaseCounter, this object has been initialized as ReadOnly."); }
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
