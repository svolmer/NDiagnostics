using NDiagnostics.Metering.Types;

namespace NDiagnostics.Metering.Samples
{
    public sealed class InstantPercentageSample : Sample
    {
        #region Constructors and Destructors

        internal InstantPercentageSample(long numerator, long denominator, TimeStamp timeStamp)
            : base(timeStamp)
        {
            this.Numerator = numerator;
            this.Denominator = denominator;
        }

        #endregion

        #region Properties

        public long Numerator { get; private set; }

        public long Denominator { get; private set; }

        #endregion
    }
}
