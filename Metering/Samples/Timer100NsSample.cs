using NDiagnostics.Metering.Types;

namespace NDiagnostics.Metering.Samples
{
    public sealed class Timer100NsSample : Sample
    {
        #region Constructors and Destructors

        internal Timer100NsSample(Time100Ns elapsedTimeOfActivity, TimeStamp100Ns timeStamp100Ns)
            : base(timeStamp100Ns)
        {
            this.ElapsedTimeOfActivity = elapsedTimeOfActivity;
        }

        #endregion

        #region Properties

        public Time100Ns ElapsedTimeOfActivity { get; private set; }

        #endregion
    }
}
