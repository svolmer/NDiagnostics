using System;
using System.Diagnostics;
using NDiagnostics.Metering.Extensions;

namespace NDiagnostics.Metering.Counters
{
    internal abstract class BaseCounter : Counter
    {
        #region Constants and Fields

        internal const string BaseSuffix = "Base";

        #endregion

        #region Constructors and Destructors

        protected BaseCounter(string categoryName, string counterName, string instanceName)
            : base(categoryName, counterName, instanceName)
        {
        }

        #endregion

        #region Methods

        internal static BaseCounter Create(string categoryName, string counterName, string instanceName)
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
            return MemoryCounter.Registry.Get<MemoryBaseCounter>(categoryName, instanceName, counterName);
        }

        #endregion
    }
}
