namespace NDiagnostics.Metering.Counters
{
    internal abstract class MemoryCounter : ICounter
    {
        #region Constructors and Destructors

        protected MemoryCounter(string categoryName, string counterName, string instanceName, InstanceLifetime instanceLifetime)
        {
            this.CategoryName = categoryName;
            this.CounterName = counterName;
            this.InstanceName = instanceName;
            this.InstanceLifetime = instanceLifetime;
            this.IsReadOnly = false;
        }

        #endregion

        #region ICounter

        public string CategoryName { get; protected set; }

        public string CounterName { get; }

        public string InstanceName { get; }

        public InstanceLifetime InstanceLifetime { get; }

        public bool IsReadOnly { get; }

        public abstract long RawValue { get; set; }

        public abstract long Increment();

        public abstract long IncrementBy(long value);

        public abstract long Decrement();

        #endregion
    }
}
