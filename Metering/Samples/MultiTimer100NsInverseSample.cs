using NDiagnostics.Metering.Types;

namespace NDiagnostics.Metering.Samples
{
    public sealed class MultiTimer100NsInverseSample : Sample
    {
        #region Constructors and Destructors

        internal MultiTimer100NsInverseSample(Time100Ns elapsedTimeOfInactivity, long count, TimeStamp timeStamp, TimeStamp100Ns timeStamp100Ns)
            : base(timeStamp, timeStamp100Ns)
        {
            this.ElapsedTimeOfInactivity = elapsedTimeOfInactivity;
            this.Count = count;
        }

        #endregion

        #region Properties

        public Time100Ns ElapsedTimeOfInactivity { get; private set; }

        public long Count { get; private set; }

        #endregion
    }
}
