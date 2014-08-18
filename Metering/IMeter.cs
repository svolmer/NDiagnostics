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

        MeterInstanceLifetime InstanceLifetime { get; }

        Sample Current { get; }

        void Reset();
    }
}
