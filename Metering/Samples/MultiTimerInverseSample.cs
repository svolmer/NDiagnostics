using NDiagnostics.Metering.Types;

namespace NDiagnostics.Metering.Samples
{
    public sealed class MultiTimerInverseSample : Sample
    {
        #region Constructors and Destructors

        internal MultiTimerInverseSample(Time elapsedTimeOfInactivity, long count, TimeStamp timeStamp)
            : base(timeStamp)
        {
            this.ElapsedTimeOfInactivity = elapsedTimeOfInactivity;
            this.Count = count;
        }

        #endregion

        #region Properties

        public Time ElapsedTimeOfInactivity { get; private set; }

        public long Count { get; private set; }

        #endregion
    }
}
