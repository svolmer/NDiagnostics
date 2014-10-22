using NDiagnostics.Metering.Samples;

namespace NDiagnostics.Metering
{
    public interface IMeter : IDisposableObject
    {
        string CategoryName { get; }

        MeterCategoryType CategoryType { get; }

        string MeterName { get; }

        MeterType MeterType { get; }

        string InstanceName { get; }

        InstanceLifetime InstanceLifetime { get; }

        bool IsReadOnly { get; }

        Sample Current { get; }

        void Reset();
    }
}
