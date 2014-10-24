using NDiagnostics.Metering.Types;

namespace NDiagnostics.Metering.Samples
{
    public sealed class TimerSample : Sample
    {
        #region Constructors and Destructors

        internal TimerSample(Time elapsedTimeOfActivity, TimeStamp timeStamp, TimeStamp100Ns timeStamp100Ns)
            : base(timeStamp, timeStamp100Ns)
        {
            this.ElapsedTimeOfActivity = elapsedTimeOfActivity;
        }

        #endregion

        #region Properties

        public Time ElapsedTimeOfActivity { get; private set; }

        #endregion
    }
}
