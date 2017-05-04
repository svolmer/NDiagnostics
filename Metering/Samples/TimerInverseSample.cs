using NDiagnostics.Metering.Types;

namespace NDiagnostics.Metering.Samples
{
    public sealed class TimerInverseSample : Sample
    {
        #region Constructors and Destructors

        internal TimerInverseSample(Time elapsedTimeOfInactivity, TimeStamp timeStamp, TimeStamp100Ns timeStamp100Ns)
            : base(timeStamp, timeStamp100Ns)
        {
            this.ElapsedTimeOfInactivity = elapsedTimeOfInactivity;
        }

        #endregion

        #region Properties

        public Time ElapsedTimeOfInactivity { get; }

        #endregion
    }
}
