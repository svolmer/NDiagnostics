using NDiagnostics.Metering.Types;

namespace NDiagnostics.Metering.Samples
{
    public sealed class InstantValueSample : Sample
    {
        #region Constructors and Destructors

        internal InstantValueSample(long value, TimeStamp timeStamp)
            : base(timeStamp)
        {
            this.Value = value;
        }

        #endregion

        #region Properties

        public long Value { get; private set; }

        #endregion
    }
}
