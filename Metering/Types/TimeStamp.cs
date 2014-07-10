using System;
using System.Diagnostics;
using System.Runtime;
using NDiagnostics.Metering.Extensions;

namespace NDiagnostics.Metering.Types
{
    [Serializable]
    public struct TimeStamp : IComparable, IComparable<TimeStamp>, IEquatable<TimeStamp>
    {
        #region Constructors and Destructors

        internal TimeStamp(long ticks)
            : this()
        {
            this.Ticks = ticks;
        }

        internal TimeStamp(float seconds)
            : this()
        {
            this.Ticks = (seconds * Stopwatch.Frequency).Round();
        }

        #endregion

        #region Properties

        public static TimeStamp Now
        {
            [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")] get { return new TimeStamp(Stopwatch.GetTimestamp()); }
        }

        internal long Ticks { [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")] get; private set; }

        private float Seconds
        {
            get { return (float) this.Ticks / Stopwatch.Frequency; }
        }

        #endregion

        #region Operators

        public static bool operator ==(TimeStamp left, TimeStamp right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(TimeStamp left, TimeStamp right)
        {
            return !left.Equals(right);
        }

        public static bool operator >(TimeStamp left, TimeStamp right)
        {
            return left.Ticks > right.Ticks;
        }

        public static bool operator >=(TimeStamp left, TimeStamp right)
        {
            return left.Ticks >= right.Ticks;
        }

        public static bool operator <(TimeStamp left, TimeStamp right)
        {
            return left.Ticks < right.Ticks;
        }

        public static bool operator <=(TimeStamp left, TimeStamp right)
        {
            return left.Ticks <= right.Ticks;
        }

        public static Time operator -(TimeStamp left, TimeStamp right)
        {
            return new Time(left.Subtract(right.Ticks));
        }

        public static TimeStamp operator -(TimeStamp left, Time right)
        {
            return new TimeStamp(left.Subtract(right.Ticks));
        }

        public static TimeStamp operator +(TimeStamp left, Time right)
        {
            return new TimeStamp(left.Add(right.Ticks));
        }

        public static implicit operator TimeStamp100Ns(TimeStamp timeStamp)
        {
            return new TimeStamp100Ns(timeStamp.Seconds);
        }

        #endregion

        #region Public Methods

        public static bool Equals(TimeStamp left, TimeStamp right)
        {
            return left.Equals(right);
        }

        public static int Compare(TimeStamp left, TimeStamp right)
        {
            return left.CompareTo(right);
        }

        public override bool Equals(object obj)
        {
            if(obj.IsNull())
            {
                return false;
            }

            return obj is TimeStamp && this.Equals((TimeStamp) obj);
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

            if(!(obj is TimeStamp))
            {
                throw new ArgumentException("Object is not a TimeStamp.", "obj");
            }

            return this.CompareTo((TimeStamp) obj);
        }

        #endregion

        #region IComparable<TimeStamp>

        public int CompareTo(TimeStamp other)
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

        #region IEquatable<TimeStamp>

        public bool Equals(TimeStamp other)
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
                throw new OverflowException("TimeStamp overflowed because the time added is too long.");
            }

            return result;
        }

        private long Subtract(long ticks)
        {
            var result = this.Ticks - ticks;
            if(((this.Ticks >> 0x3f) != (ticks >> 0x3f)) && ((this.Ticks >> 0x3f) != (result >> 0x3f)))
            {
                throw new OverflowException("TimeStamp overflowed because the time subtracted is too long.");
            }

            return result;
        }

        #endregion
    }
}
