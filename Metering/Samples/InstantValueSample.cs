using NDiagnostics.Metering.Types;

namespace NDiagnostics.Metering.Samples
{
    public sealed class InstantValueSample : Sample
    {
        #region Constructors and Destructors

        internal InstantValueSample(long value, TimeStamp timeStamp, TimeStamp100Ns timeStamp100Ns)
            : base(timeStamp, timeStamp100Ns)
        {
            this.Value = value;
        }

        #endregion

        #region Properties

        public long Value { get; private set; }

        #endregion
    }
}
