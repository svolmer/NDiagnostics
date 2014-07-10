using NDiagnostics.Metering.Types;

namespace NDiagnostics.Metering.Samples
{
    public sealed class AverageRateSample : Sample
    {
        #region Constructors and Destructors

        internal AverageRateSample(long count, TimeStamp timeStamp)
            : base(timeStamp)
        {
            this.Count = count;
        }

        #endregion

        #region Properties

        public long Count { get; private set; }

        #endregion
    }
}
