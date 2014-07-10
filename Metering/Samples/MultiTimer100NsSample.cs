using NDiagnostics.Metering.Types;

namespace NDiagnostics.Metering.Samples
{
    public sealed class MultiTimer100NsSample : Sample
    {
        #region Constructors and Destructors

        internal MultiTimer100NsSample(Time100Ns elapsedTimeOfActivity, long count, TimeStamp100Ns timeStamp100Ns)
            : base(timeStamp100Ns)
        {
            this.ElapsedTimeOfActivity = elapsedTimeOfActivity;
            this.Count = count;
        }

        #endregion

        #region Properties

        public Time100Ns ElapsedTimeOfActivity { get; private set; }

        public long Count { get; private set; }

        #endregion
    }
}
