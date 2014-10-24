using System;
using NDiagnostics.Metering.Samples;

namespace NDiagnostics.Metering
{
    public interface IInstantTime : IMeter
    {
        void Start();

        void Start(DateTime timeStamp);

        new InstantTimeSample Current { get; }
    }
}
