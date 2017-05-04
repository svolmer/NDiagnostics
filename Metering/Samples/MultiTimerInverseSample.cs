using NDiagnostics.Metering.Types;

namespace NDiagnostics.Metering.Samples
{
    public sealed class MultiTimerInverseSample : Sample
    {
        #region Constructors and Destructors

        internal MultiTimerInverseSample(Time elapsedTimeOfInactivity, long count, TimeStamp timeStamp, TimeStamp100Ns timeStamp100Ns)
            : base(timeStamp, timeStamp100Ns)
        {
            this.ElapsedTimeOfInactivity = elapsedTimeOfInactivity;
            this.Count = count;
        }

        #endregion

        #region Properties

        public Time ElapsedTimeOfInactivity { get; }

        public long Count { get; }

        #endregion
    }
}
