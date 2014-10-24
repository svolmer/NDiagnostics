using NDiagnostics.Metering.Types;

namespace NDiagnostics.Metering.Counters
{
    internal struct RawSample
    {
        #region Constructors and Destructors

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

        internal long Value { get; private set; }

        internal long BaseValue { get; private set; }

        internal TimeStamp TimeStamp { get; private set; }

        internal TimeStamp100Ns TimeStamp100Ns { get; private set; }

        #endregion
    }
}
