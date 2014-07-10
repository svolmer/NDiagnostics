using System;

namespace NDiagnostics.Metering
{
    public interface IDisposableObject : IDisposable
    {
        bool IsDisposed { get; }
    }
}
