using NDiagnostics.Metering.Types;

namespace NDiagnostics.Metering.Samples
{
    public sealed class AverageTimeSample : Sample
    {
        #region Constructors and Destructors

        internal AverageTimeSample(Time elapsedTime, long count, TimeStamp timeStamp, TimeStamp100Ns timeStamp100Ns)
            : base(timeStamp, timeStamp100Ns)
        {
            this.ElapsedTime = elapsedTime;
            this.Count = count;
        }

        #endregion

        #region Properties

        public Time ElapsedTime { get; private set; }

        public long Count { get; private set; }

        #endregion
    }
}
