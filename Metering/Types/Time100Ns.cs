using System;
using System.Runtime;
using NDiagnostics.Metering.Extensions;

namespace NDiagnostics.Metering.Types
{
    [Serializable]
    public struct Time100Ns : IComparable, IComparable<Time100Ns>, IEquatable<Time100Ns>
    {
        #region Constructors and Destructors

        internal Time100Ns(long ticks)
            : this()
        {
            this.Ticks = ticks;
        }

        internal Time100Ns(float seconds)
            : this()
        {
            this.Ticks = (seconds * 10000000).Round();
        }

        #endregion

        #region Properties

        internal long Ticks { [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")] get; private set; }

        internal float Seconds
        {
            get { return (float) this.Ticks / 10000000; }
        }

        #endregion

        #region Operators

        public static bool operator ==(Time100Ns left, Time100Ns right)
        {
            return left.Ticks == right.Ticks;
        }

        public static bool operator !=(Time100Ns left, Time100Ns right)
        {
            return left.Ticks != right.Ticks;
        }

        public static bool operator >(Time100Ns left, Time100Ns right)
        {
            return left.Ticks > right.Ticks;
        }

        public static bool operator >=(Time100Ns left, Time100Ns right)
        {
            return left.Ticks >= right.Ticks;
        }

        public static bool operator <(Time100Ns left, Time100Ns right)
        {
            return left.Ticks < right.Ticks;
        }

        public static bool operator <=(Time100Ns left, Time100Ns right)
        {
            return left.Ticks <= right.Ticks;
        }

        public static Time100Ns operator +(Time100Ns time)
        {
            return time;
        }

        public static Time100Ns operator +(Time100Ns left, Time100Ns right)
        {
            return new Time100Ns(left.Add(right.Ticks));
        }

        public static Time100Ns operator -(Time100Ns time)
        {
            if(time.Ticks == long.MinValue)
            {
                throw new OverflowException("Negating the minimum value of Time100Ns is invalid.");
            }

            return new Time100Ns(-time.Ticks);
        }

        public static Time100Ns operator -(Time100Ns left, Time100Ns right)
        {
            return new Time100Ns(left.Subtract(right.Ticks));
        }

        public static implicit operator Time(Time100Ns time)
        {
            return new Time(time.Seconds);
        }

        #endregion

        #region Public Methods

        public static Time100Ns FromSeconds(float seconds)
        {
            return new Time100Ns(seconds);
        }

        public static bool Equals(Time100Ns left, Time100Ns right)
        {
            return left.Equals(right);
        }

        public static int Compare(Time100Ns left, Time100Ns right)
        {
            return left.CompareTo(right);
        }

        public override bool Equals(object obj)
        {
            if(obj.IsNull())
            {
                return false;
            }

            return obj is Time100Ns && this.Equals((Time100Ns) obj);
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

            if(!(obj is Time100Ns))
            {
                throw new ArgumentException("Object is not a Time100Ns.", "obj");
            }

            return this.CompareTo((Time100Ns) obj);
        }

        #endregion

        #region IComparable<Time100Ns>

        public int CompareTo(Time100Ns other)
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

        #region IEquatable<Time100Ns>

        public bool Equals(Time100Ns other)
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
                throw new OverflowException("Time100Ns overflowed because the time added is too long.");
            }

            return result;
        }

        private long Subtract(long ticks)
        {
            var result = this.Ticks - ticks;
            if(((this.Ticks >> 0x3f) != (ticks >> 0x3f)) && ((this.Ticks >> 0x3f) != (result >> 0x3f)))
            {
                throw new OverflowException("Time100Ns overflowed because the time subtracted is too long.");
            }

            return result;
        }

        #endregion
    }
}
