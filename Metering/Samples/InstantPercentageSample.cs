using NDiagnostics.Metering.Types;

namespace NDiagnostics.Metering.Samples
{
    public sealed class InstantPercentageSample : Sample
    {
        #region Constructors and Destructors

        internal InstantPercentageSample(long numerator, long denominator, TimeStamp timeStamp, TimeStamp100Ns timeStamp100Ns)
            : base(timeStamp, timeStamp100Ns)
        {
            this.Numerator = numerator;
            this.Denominator = denominator;
        }

        #endregion

        #region Properties

        public long Numerator { get; }

        public long Denominator { get; }

        #endregion
    }
}
