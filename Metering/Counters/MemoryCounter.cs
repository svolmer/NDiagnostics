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
        }

        #endregion

        #region ICounter

        public string CategoryName { get; protected set; }

        public string CounterName { get; private set; }

        public string InstanceName { get; private set; }

        public InstanceLifetime InstanceLifetime { get; private set; }

        public abstract long RawValue { get; set; }

        public abstract long Increment();

        public abstract long IncrementBy(long value);

        public abstract long Decrement();

        #endregion
    }
}
