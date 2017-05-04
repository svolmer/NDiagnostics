using System;
using System.Collections.Generic;

namespace NDiagnostics.Metering.Counters
{
    internal sealed class MemoryCounterRegistry
    {
        #region Constants and Fields

        private static readonly Lazy<MemoryCounterRegistry> instance = new Lazy<MemoryCounterRegistry>(() => new MemoryCounterRegistry(), true);

        private static IDictionary<string, IDictionary<string, IDictionary<string, ICounter>>> counters;

        #endregion

        #region Constructors and Destructors

        private MemoryCounterRegistry()
        {
            counters = new Dictionary<string, IDictionary<string, IDictionary<string, ICounter>>>();
        }

        #endregion

        #region Properties

        public static MemoryCounterRegistry Instance => instance.Value;

        #endregion

        #region Public Methods

        public T Get<T>(string categoryName, string counterName, string instanceName, InstanceLifetime instanceLifetime, bool isReadOnly, IBaseCounter baseCounter = null)
            where T : class, ICounter
        {
            if(!counters.ContainsKey(categoryName))
            {
                counters.Add(categoryName, new Dictionary<string, IDictionary<string, ICounter>>());
            }
            if(!counters[categoryName].ContainsKey(instanceName))
            {
                counters[categoryName].Add(instanceName, new Dictionary<string, ICounter>());
            }
            if(!counters[categoryName][instanceName].ContainsKey(counterName))
            {
                if(typeof(T) == typeof(MemoryBaseCounter))
                {
                    counters[categoryName][instanceName].Add(counterName, new MemoryBaseCounter(categoryName, counterName, instanceName, instanceLifetime));
                }
                else if(typeof(T) == typeof(MemoryValueCounter))
                {
                    counters[categoryName][instanceName].Add(counterName, new MemoryValueCounter(categoryName, counterName, instanceName, instanceLifetime, baseCounter));
                }
            }

            var counter = counters[categoryName][instanceName][counterName];
            if(isReadOnly)
            {
                counter = counter.ReadOnly();
            }
            return counter as T;
        }

        public void Remove(string categoryName, string counterName, string instanceName)
        {
            if(counters.ContainsKey(categoryName))
            {
                if(counters[categoryName].ContainsKey(instanceName))
                {
                    if(counters[categoryName][instanceName].ContainsKey(instanceName))
                    {
                        counters[categoryName][instanceName].Remove(counterName);
                    }
                }
            }
        }

        #endregion
    }
}
