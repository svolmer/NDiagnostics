namespace NDiagnostics.Metering.Counters
{
    internal interface IValueCounter : ICounter
    {
        IBaseCounter BaseCounter { get; }

        RawSample RawSample { get; }
    }
}
