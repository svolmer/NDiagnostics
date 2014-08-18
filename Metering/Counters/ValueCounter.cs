using System;
using System.Diagnostics;
using NDiagnostics.Metering.Extensions;

namespace NDiagnostics.Metering.Counters
{
    internal static class ValueCounter
    {
        #region Methods

        internal static IValueCounter Create(string categoryName, string counterName, string instanceName, IBaseCounter baseCounter = null)
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
            return MemoryCounterRegistry.Instance.Get<MemoryValueCounter>(categoryName, counterName, instanceName, baseCounter);
        }

        #endregion
    }
}
