using NDiagnostics.Metering.Types;

namespace NDiagnostics.Metering.Samples
{
    public sealed class Timer100NsInverseSample : Sample
    {
        #region Constructors and Destructors

        internal Timer100NsInverseSample(Time100Ns elapsedTimeOfInactivity, TimeStamp timeStamp, TimeStamp100Ns timeStamp100Ns)
            : base(timeStamp, timeStamp100Ns)
        {
            this.ElapsedTimeOfInactivity = elapsedTimeOfInactivity;
        }

        #endregion

        #region Properties

        public Time100Ns ElapsedTimeOfInactivity { get; private set; }

        #endregion
    }
}
