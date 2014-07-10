using System;
using System.Diagnostics;
using NDiagnostics.Metering.Extensions;

namespace NDiagnostics.Metering.Counters
{
    internal abstract class ValueCounter : Counter
    {
        #region Constructors and Destructors

        protected ValueCounter(string categoryName, string counterName, string instanceName, BaseCounter baseCounter)
            : base(categoryName, counterName, instanceName)
        {
            this.BaseCounter = baseCounter;
        }

        #endregion

        #region Properties

        public BaseCounter BaseCounter { get; private set; }

        public abstract RawSample RawSample { get; }

        #endregion

        #region Methods

        internal static ValueCounter Create(string categoryName, string counterName, string instanceName, BaseCounter baseCounter = null)
        {
            categoryName.ThrowIfNullOrEmpty("categoryName");
            counterName.ThrowIfNullOrEmpty("counterName");

            try
            {
                if(PerformanceCounterCategory.Exists(categoryName) && PerformanceCounterCategory.CounterExists(counterName, categoryName))
                {
                    return new SystemValueCounter(categoryName, counterName, instanceName, baseCounter);
                }
            }
            catch(UnauthorizedAccessException)
            {
            }
            return MemoryCounter.Registry.Get<MemoryValueCounter>(categoryName, counterName, instanceName, baseCounter);
        }

        #endregion
    }
}
