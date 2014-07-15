using NDiagnostics.Metering.Types;

namespace NDiagnostics.Metering.Samples
{
    public sealed class SampleRateSample : Sample
    {
        #region Constructors and Destructors

        internal SampleRateSample(long count, TimeStamp timeStamp)
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
