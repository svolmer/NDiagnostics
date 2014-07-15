using NDiagnostics.Metering.Types;

namespace NDiagnostics.Metering.Samples
{
    public sealed class SamplePercentageSample : Sample
    {
        #region Constructors and Destructors

        internal SamplePercentageSample(long numerator, long denominator, TimeStamp timeStamp)
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
