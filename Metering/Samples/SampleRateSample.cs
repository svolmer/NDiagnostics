using NDiagnostics.Metering.Types;

namespace NDiagnostics.Metering.Samples
{
    public sealed class SampleRateSample : Sample
    {
        #region Constructors and Destructors

        internal SampleRateSample(long count, TimeStamp timeStamp, TimeStamp100Ns timeStamp100Ns)
            : base(timeStamp, timeStamp100Ns)
        {
            this.Count = count;
        }

        #endregion

        #region Properties

        public long Count { get; }

        #endregion
    }
}
