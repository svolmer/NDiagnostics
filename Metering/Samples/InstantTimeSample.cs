using NDiagnostics.Metering.Types;

namespace NDiagnostics.Metering.Samples
{
    public sealed class InstantTimeSample : Sample
    {
        #region Constructors and Destructors

        internal InstantTimeSample(TimeStamp startTime, TimeStamp timeStamp)
            : base(timeStamp)
        {
            this.StartTime = startTime;
        }

        #endregion

        #region Properties

        public TimeStamp StartTime { get; private set; }

        #endregion
    }
}
