namespace NDiagnostics.Metering.Counters
{
    internal interface ICounter
    {
        string CategoryName { get; }

        string CounterName { get; }

        string InstanceName { get; }

        long RawValue { get; set; }

        long Increment();

        long IncrementBy(long value);

        long Decrement();
    }
}
