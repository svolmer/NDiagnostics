namespace NDiagnostics.Metering.Counters
{
    internal interface ICounter
    {
        string CategoryName { get; }

        string CounterName { get; }

        string InstanceName { get; }

        InstanceLifetime InstanceLifetime { get; }

        bool IsReadOnly { get; }

        long RawValue { get; set; }

        long Increment();

        long IncrementBy(long value);

        long Decrement();
    }
}
