using NDiagnostics.Metering.Types;

namespace NDiagnostics.Metering.Samples
{
    public sealed class AverageValueSample : Sample
    {
        #region Constructors and Destructors

        internal AverageValueSample(long value, long count, TimeStamp timeStamp, TimeStamp100Ns timeStamp100Ns)
            : base(timeStamp, timeStamp100Ns)
        {
            this.Value = value;
            this.Count = count;
        }

        #endregion

        #region Properties

        public long Value { get; }

        public long Count { get; }

        #endregion
    }
}
