using System;
using System.Runtime;
using NDiagnostics.Metering.Extensions;

namespace NDiagnostics.Metering.Types
{
    [Serializable]
    public struct TimeStamp100Ns : IComparable, IComparable<TimeStamp100Ns>, IEquatable<TimeStamp100Ns>
    {
        #region Constructors and Destructors

        internal TimeStamp100Ns(long ticks)
            : this()
        {
            this.Ticks = ticks;
        }

        internal TimeStamp100Ns(DateTime dateTime)
            : this()
        {
            this.Ticks = dateTime.ToFileTime();
        }

        #endregion

        #region Properties

        public static TimeStamp100Ns Now
        {
            get { return new TimeStamp100Ns(DateTime.Now); }
        }

        internal long Ticks { [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")] get; private set; }

        #endregion

        #region Operators

        public static bool operator ==(TimeStamp100Ns left, TimeStamp100Ns right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(TimeStamp100Ns left, TimeStamp100Ns right)
        {
            return !left.Equals(right);
        }

        public static bool operator >(TimeStamp100Ns left, TimeStamp100Ns right)
        {
            return left.Ticks > right.Ticks;
        }

        public static bool operator >=(TimeStamp100Ns left, TimeStamp100Ns right)
        {
            return left.Ticks >= right.Ticks;
        }

        public static bool operator <(TimeStamp100Ns left, TimeStamp100Ns right)
        {
            return left.Ticks < right.Ticks;
        }

        public static bool operator <=(TimeStamp100Ns left, TimeStamp100Ns right)
        {
            return left.Ticks <= right.Ticks;
        }

        public static Time100Ns operator -(TimeStamp100Ns left, TimeStamp100Ns right)
        {
            return new Time100Ns(left.Subtract(right.Ticks));
        }

        public static TimeStamp100Ns operator -(TimeStamp100Ns left, Time100Ns right)
        {
            return new TimeStamp100Ns(left.Subtract(right.Ticks));
        }

        public static TimeStamp100Ns operator +(TimeStamp100Ns left, Time100Ns right)
        {
            return new TimeStamp100Ns(left.Add(right.Ticks));
        }

        #endregion

        #region Public Methods

        public static TimeStamp100Ns FromDateTime(DateTime dateTime)
        {
            return new TimeStamp100Ns(dateTime);
        }

        public static bool Equals(TimeStamp100Ns left, TimeStamp100Ns right)
        {
            return left.Equals(right);
        }

        public static int Compare(TimeStamp100Ns left, TimeStamp100Ns right)
        {
            return left.CompareTo(right);
        }

        public override bool Equals(object obj)
        {
            if(obj.IsNull())
            {
                return false;
            }

            return obj is TimeStamp100Ns && this.Equals((TimeStamp100Ns) obj);
        }

        public override int GetHashCode()
        {
            return this.Ticks.GetHashCode();
        }

        #endregion

        #region IComparable

        public int CompareTo(object obj)
        {
            if(obj.IsNull())
            {
                return 1;
            }

            if(!(obj is TimeStamp100Ns))
            {
                throw new ArgumentException("Object is not a TimeStamp100Ns.", "obj");
            }

            return this.CompareTo((TimeStamp100Ns) obj);
        }

        #endregion

        #region IComparable<TimeStamp100Ns>

        public int CompareTo(TimeStamp100Ns other)
        {
            if(this.Ticks > other.Ticks)
            {
                return 1;
            }

            if(this.Ticks < other.Ticks)
            {
                return -1;
            }

            return 0;
        }

        #endregion

        #region IEquatable<TimeStamp100Ns>

        public bool Equals(TimeStamp100Ns other)
        {
            return this.Ticks == other.Ticks;
        }

        #endregion

        #region Methods

        private long Add(long ticks)
        {
            var result = this.Ticks + ticks;
            if(((this.Ticks >> 0x3f) == (ticks >> 0x3f)) && ((this.Ticks >> 0x3f) != (result >> 0x3f)))
            {
                throw new OverflowException("TimeStamp100Ns overflowed because the time added is too long.");
            }

            return result;
        }

        private long Subtract(long ticks)
        {
            var result = this.Ticks - ticks;
            if(((this.Ticks >> 0x3f) != (ticks >> 0x3f)) && ((this.Ticks >> 0x3f) != (result >> 0x3f)))
            {
                throw new OverflowException("TimeStamp100Ns overflowed because the time subtracted is too long.");
            }

            return result;
        }

        #endregion
    }
}
