using NDiagnostics.Metering.Types;

namespace NDiagnostics.Metering.Counters
{
    internal struct RawSample
    {
        #region Constructors and Destructors

        internal RawSample(long value, long baseValue, TimeStamp timeStamp)
            : this()
        {
            this.Value = value;
            this.BaseValue = baseValue;
            this.TimeStamp = timeStamp;
            this.TimeStamp100Ns = timeStamp;
        }

        internal RawSample(long value, long baseValue, TimeStamp timeStamp, TimeStamp100Ns timeStamp100Ns)
            : this()
        {
            this.Value = value;
            this.BaseValue = baseValue;
            this.TimeStamp = timeStamp;
            this.TimeStamp100Ns = timeStamp100Ns;
        }

        #endregion

        #region Properties

        public long Value { get; private set; }

        public long BaseValue { get; private set; }

        public TimeStamp TimeStamp { get; private set; }

        public TimeStamp100Ns TimeStamp100Ns { get; private set; }

        #endregion
    }
}
