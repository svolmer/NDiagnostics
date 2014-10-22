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

        public string CategoryName
        {
            get { return this.valueCounter.CategoryName; }
        }

        public string CounterName
        {
            get { return this.valueCounter.CounterName; }
        }

        public string InstanceName
        {
            get { return this.valueCounter.InstanceName; }
        }

        public InstanceLifetime InstanceLifetime
        {
            get { return this.valueCounter.InstanceLifetime; }
        }

        public bool IsReadOnly
        {
            get { return true; }
        }

        public long RawValue
        {
            get { return this.valueCounter.RawValue; }
            set { throw new InvalidOperationException("Cannot update ValueCounter, this object has been initialized as ReadOnly."); }
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

        public IBaseCounter BaseCounter
        {
            get { return this.valueCounter.BaseCounter.ReadOnly(); }
        }

        public RawSample RawSample
        {
            get { return this.valueCounter.RawSample; }
        }

        #endregion
    }
}
