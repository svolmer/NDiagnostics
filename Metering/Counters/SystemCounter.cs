﻿using NDiagnostics.Metering.Extensions;

namespace NDiagnostics.Metering.Counters
{
    internal abstract class SystemCounter : DisposableObject, ICounter
    {
        #region Constructors and Destructors

        protected SystemCounter(string categoryName, string counterName, string instanceName)
        {
            instanceName.ThrowIfNull("instanceName");
            instanceName.ThrowIfExceedsMaxSize("instanceName", 127);

            this.CategoryName = categoryName;
            this.CounterName = counterName;
            this.InstanceName = instanceName;
        }

        #endregion

        #region ICounter

        public string CategoryName { get; protected set; }

        public string CounterName { get; private set; }

        public string InstanceName { get; private set; }

        public abstract long RawValue { get; set; }

        public abstract long Increment();

        public abstract long IncrementBy(long value);

        public abstract long Decrement();

        #endregion
    }
}