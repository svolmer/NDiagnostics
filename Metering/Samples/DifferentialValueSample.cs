using NDiagnostics.Metering.Types;

namespace NDiagnostics.Metering.Samples
{
    public sealed class DifferentialValueSample : Sample
    {
        #region Constructors and Destructors

        internal DifferentialValueSample(long value, TimeStamp timeStamp, TimeStamp100Ns timeStamp100Ns)
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
