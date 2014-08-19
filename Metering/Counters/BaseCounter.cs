using System;
using System.Diagnostics;

namespace NDiagnostics.Metering.Counters
{
    internal static class BaseCounter
    {
        #region Constants and Fields

        internal const string BaseSuffix = "Base";

        #endregion

        #region Methods

        internal static IBaseCounter Create(string categoryName, string counterName, string instanceName, InstanceLifetime instanceLifetime)
        {
            counterName = counterName + BaseSuffix;
            try
            {
                if(PerformanceCounterCategory.Exists(categoryName) && PerformanceCounterCategory.CounterExists(counterName, categoryName))
                {
                    return new SystemBaseCounter(categoryName, counterName, instanceName, instanceLifetime);
                }
            }
            catch(Exception)
            {
            }
            return MemoryCounterRegistry.Instance.Get<MemoryBaseCounter>(categoryName, counterName, instanceName, instanceLifetime);
        }

        #endregion
    }
}
