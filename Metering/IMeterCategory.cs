namespace NDiagnostics.Metering
{
    public interface IMeterCategory : IDisposableObject
    {
        string CategoryName { get; }

        MeterCategoryType CategoryType { get; }

        string[] InstanceNames { get; }
    }

    public interface IMeterCategory<in T> : IMeterCategory
    {
        IMeter this[T meterName] { get; }

        IMeter this[T meterName, string instanceName] { get; }
    }
}
