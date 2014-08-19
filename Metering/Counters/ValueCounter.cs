using System;
using System.Diagnostics;

namespace NDiagnostics.Metering.Counters
{
    internal static class ValueCounter
    {
        #region Methods

        internal static IValueCounter Create(string categoryName, string counterName, string instanceName, InstanceLifetime instanceLifetime, IBaseCounter baseCounter = null)
        {
            try
            {
                if(PerformanceCounterCategory.Exists(categoryName) && PerformanceCounterCategory.CounterExists(counterName, categoryName))
                {
                    return new SystemValueCounter(categoryName, counterName, instanceName, instanceLifetime, baseCounter);
                }
            }
            catch(Exception)
            {
            }
            return MemoryCounterRegistry.Instance.Get<MemoryValueCounter>(categoryName, counterName, instanceName, instanceLifetime, baseCounter);
        }

        #endregion
    }
}
