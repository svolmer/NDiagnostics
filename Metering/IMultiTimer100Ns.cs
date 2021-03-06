﻿using NDiagnostics.Metering.Samples;
using NDiagnostics.Metering.Types;

namespace NDiagnostics.Metering
{
    public interface IMultiTimer100Ns : IMeter<MultiTimer100NsSample>
    {
        void Sample(Time100Ns elapsedTimeOfActivity);
    }
}
