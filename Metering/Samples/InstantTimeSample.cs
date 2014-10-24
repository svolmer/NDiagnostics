using NDiagnostics.Metering.Types;

namespace NDiagnostics.Metering.Samples
{
    public sealed class InstantTimeSample : Sample
    {
        #region Constructors and Destructors

        internal InstantTimeSample(TimeStamp100Ns startTime, TimeStamp timeStamp, TimeStamp100Ns timeStamp100Ns)
            : base(timeStamp, timeStamp100Ns)
        {
            this.StartTime = startTime;
        }

        #endregion

        #region Properties

        public TimeStamp100Ns StartTime { get; private set; }

        #endregion
    }
}
