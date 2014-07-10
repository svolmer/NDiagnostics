using System;
using System.Collections.Generic;

namespace NDiagnostics.Metering.Counters
{
    internal sealed class MemoryCounter
    {
        #region Constants and Fields

        private static readonly Lazy<MemoryCounter> instance = new Lazy<MemoryCounter>(() => new MemoryCounter(), true);

        private static IDictionary<string, IDictionary<string, IDictionary<string, Counter>>> counters;

        #endregion

        #region Constructors and Destructors

        private MemoryCounter()
        {
            counters = new Dictionary<string, IDictionary<string, IDictionary<string, Counter>>>();
        }

        #endregion

        #region Properties

        public static MemoryCounter Registry
        {
            get { return instance.Value; }
        }

        #endregion

        #region Public Methods

        public T Get<T>(string categoryName, string counterName, string instanceName, BaseCounter baseCounter = null) where T : Counter
        {
            if(!counters.ContainsKey(categoryName))
            {
                counters.Add(categoryName, new Dictionary<string, IDictionary<string, Counter>>());
            }
            if(!counters[categoryName].ContainsKey(instanceName))
            {
                counters[categoryName].Add(instanceName, new Dictionary<string, Counter>());
            }
            if(!counters[categoryName][instanceName].ContainsKey(counterName))
            {
                if(typeof(T) == typeof(MemoryBaseCounter))
                {
                    counters[categoryName][instanceName].Add(counterName, new MemoryBaseCounter(categoryName, counterName, instanceName));
                }
                else if(typeof(T) == typeof(MemoryValueCounter))
                {
                    counters[categoryName][instanceName].Add(counterName, new MemoryValueCounter(categoryName, counterName, instanceName, baseCounter));
                }
            }
            return counters[categoryName][instanceName][counterName] as T;
        }

        #endregion
    }
}
