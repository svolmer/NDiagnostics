using System;
using NDiagnostics.Metering.Samples;

namespace NDiagnostics.Metering
{
    public interface IInstantTime : IMeter<InstantTimeSample>
    {
        void Start();

        void Start(DateTime timeStamp);
    }
}
