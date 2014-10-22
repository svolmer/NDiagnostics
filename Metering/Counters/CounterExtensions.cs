namespace NDiagnostics.Metering.Counters
{
    internal static class CounterExtensions
    {
        #region Methods

        internal static ICounter ReadOnly (this ICounter counter) 
        {
            if(counter is IBaseCounter)
            {
                var baseCounter = counter as IBaseCounter;
                return baseCounter.ReadOnly();
            }
            if(counter is IValueCounter)
            {
                var valueCounter = counter as IValueCounter;
                return valueCounter.ReadOnly();
            }
            return null;
        }

        internal static IBaseCounter ReadOnly(this IBaseCounter baseCounter)
        {
            return new ReadOnlyBaseCounter(baseCounter);
        }

        internal static IValueCounter ReadOnly(this IValueCounter valueCounter)
        {
            return new ReadOnlyValueCounter(valueCounter);
        }

        #endregion
    }
}
