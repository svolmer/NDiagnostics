using NDiagnostics.Metering.Types;

namespace NDiagnostics.Metering.Samples
{
    public sealed class MultiTimerSample : Sample
    {
        #region Constructors and Destructors

        internal MultiTimerSample(Time elapsedTimeOfActivity, long count, TimeStamp timeStamp)
            : base(timeStamp)
        {
            this.ElapsedTimeOfActivity = elapsedTimeOfActivity;
            this.Count = count;
        }

        #endregion

        #region Properties

        public Time ElapsedTimeOfActivity { get; private set; }

        public long Count { get; private set; }

        #endregion
    }
}
