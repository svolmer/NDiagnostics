using NDiagnostics.Metering.Types;

namespace NDiagnostics.Metering.Samples
{
    public sealed class MultiTimerSample : Sample
    {
        #region Constructors and Destructors

        internal MultiTimerSample(Time elapsedTimeOfActivity, long count, TimeStamp timeStamp, TimeStamp100Ns timeStamp100Ns)
            : base(timeStamp, timeStamp100Ns)
        {
            this.ElapsedTimeOfActivity = elapsedTimeOfActivity;
            this.Count = count;
        }

        #endregion

        #region Properties

        public Time ElapsedTimeOfActivity { get; }

        public long Count { get; }

        #endregion
    }
}
