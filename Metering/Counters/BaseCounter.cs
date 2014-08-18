using System;
using System.Diagnostics;
using NDiagnostics.Metering.Extensions;

namespace NDiagnostics.Metering.Counters
{
    internal static class BaseCounter
    {
        #region Constants and Fields

        internal const string BaseSuffix = "Base";

        #endregion

        #region Methods

        internal static IBaseCounter Create(string categoryName, string counterName, string instanceName)
        {
            categoryName.ThrowIfNullOrEmpty("categoryName");
            counterName.ThrowIfNullOrEmpty("counterName");

            counterName = counterName + BaseSuffix;

            try
            {
                if(PerformanceCounterCategory.Exists(categoryName) && PerformanceCounterCategory.CounterExists(counterName, categoryName))
                {
                    return new SystemBaseCounter(categoryName, counterName, instanceName);
                }
            }
            catch(UnauthorizedAccessException)
            {
            }
            return MemoryCounterRegistry.Instance.Get<MemoryBaseCounter>(categoryName, instanceName, counterName);
        }

        #endregion
    }
}
