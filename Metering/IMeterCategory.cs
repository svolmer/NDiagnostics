using System;

namespace NDiagnostics.Metering
{
    public interface IMeterCategory : IDisposableObject
    {
        string CategoryName { get; }

        MeterCategoryType CategoryType { get; }

        string[] InstanceNames { get; }
    }

    public interface IMeterCategory<in TEnum> : IMeterCategory where TEnum : struct, IConvertible, IComparable, IFormattable
    {
        IMeter this[TEnum meterName] { get; }

        IMeter this[TEnum meterName, string instanceName] { get; }

        void CreateInstance(string instanceName, InstanceLifetime lifetime = InstanceLifetime.Global);

        void RemoveInstance(string instanceName);
    }
}
