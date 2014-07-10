using NDiagnostics.Metering.Types;

namespace NDiagnostics.Metering.Samples
{
    public sealed class TimerSample : Sample
    {
        #region Constructors and Destructors

        internal TimerSample(Time elapsedTimeOfActivity, TimeStamp timeStamp)
            : base(timeStamp)
        {
            this.ElapsedTimeOfActivity = elapsedTimeOfActivity;
        }

        #endregion

        #region Properties

        public Time ElapsedTimeOfActivity { get; private set; }

        #endregion
    }
}
