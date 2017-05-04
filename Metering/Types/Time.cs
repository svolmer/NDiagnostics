using System;
using System.Diagnostics;
using System.Runtime;
using NDiagnostics.Metering.Extensions;

namespace NDiagnostics.Metering.Types
{
    [Serializable]
    public struct Time : IComparable, IComparable<Time>, IEquatable<Time>
    {
        #region Constructors and Destructors

        internal Time(long ticks)
            : this()
        {
            this.Ticks = ticks;
        }

        internal Time(double seconds)
            : this()
        {
            this.Ticks = (seconds * Stopwatch.Frequency).Round();
        }

        #endregion

        #region Properties

        internal long Ticks { [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")] get; }

        internal double Seconds => ((double) this.Ticks) / Stopwatch.Frequency;

        #endregion

        #region Operators

        public static bool operator ==(Time left, Time right)
        {
            return left.Ticks == right.Ticks;
        }

        public static bool operator !=(Time left, Time right)
        {
            return left.Ticks != right.Ticks;
        }

        public static bool operator >(Time left, Time right)
        {
            return left.Ticks > right.Ticks;
        }

        public static bool operator >=(Time left, Time right)
        {
            return left.Ticks >= right.Ticks;
        }

        public static bool operator <(Time left, Time right)
        {
            return left.Ticks < right.Ticks;
        }

        public static bool operator <=(Time left, Time right)
        {
            return left.Ticks <= right.Ticks;
        }

        public static Time operator +(Time time)
        {
            return time;
        }

        public static Time operator +(Time left, Time right)
        {
            return new Time(left.Add(right.Ticks));
        }

        public static Time operator -(Time time)
        {
            if(time.Ticks == long.MinValue)
            {
                throw new OverflowException("Negating the minimum value of Time is invalid.");
            }

            return new Time(-time.Ticks);
        }

        public static Time operator -(Time left, Time right)
        {
            return new Time(left.Subtract(right.Ticks));
        }

        #endregion

        #region Public Methods

        public static Time FromSeconds(double seconds)
        {
            return new Time(seconds);
        }

        public static bool Equals(Time left, Time right)
        {
            return left.Equals(right);
        }

        public static int Compare(Time left, Time right)
        {
            return left.CompareTo(right);
        }

        public override bool Equals(object obj)
        {
            if(obj.IsNull())
            {
                return false;
            }

            return obj is Time && this.Equals((Time) obj);
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

            if(!(obj is Time))
            {
                throw new ArgumentException("Object is not a Time.", nameof(obj));
            }

            return this.CompareTo((Time) obj);
        }

        #endregion

        #region IComparable<Time>

        public int CompareTo(Time other)
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

        #region IEquatable<Time>

        public bool Equals(Time other)
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
                throw new OverflowException("Time overflowed because the time added is too long.");
            }

            return result;
        }

        private long Subtract(long ticks)
        {
            var result = this.Ticks - ticks;
            if(((this.Ticks >> 0x3f) != (ticks >> 0x3f)) && ((this.Ticks >> 0x3f) != (result >> 0x3f)))
            {
                throw new OverflowException("Time overflowed because the time subtracted is too long.");
            }

            return result;
        }

        #endregion
    }
}
