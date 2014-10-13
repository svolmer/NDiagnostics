using NDiagnostics.Metering.Extensions;
using NDiagnostics.Metering.Types;

namespace NDiagnostics.Metering.Samples
{
    public abstract class Sample
    {
        #region Constructors and Destructors

        protected Sample(TimeStamp timeStamp)
        {
            this.TimeStamp = timeStamp;
            this.TimeStamp100Ns = timeStamp;
        }

        protected Sample(TimeStamp100Ns timeStamp100Ns)
        {
            this.TimeStamp = timeStamp100Ns;
            this.TimeStamp100Ns = timeStamp100Ns;
        }

        #endregion

        #region Properties

        internal TimeStamp TimeStamp { get; private set; }

        internal TimeStamp100Ns TimeStamp100Ns { get; private set; }

        #endregion

        #region Public Methods

        // Instant Meters 

        public static long ComputeValue(InstantValueSample sample)
        {
            sample.ThrowIfNull("sample");
            return sample.Value;
        }

        public static float ComputeValue(InstantTimeSample sample)
        {
            sample.ThrowIfNull("sample");
            return (sample.TimeStamp - sample.StartTime).Seconds;
        }

        public static float ComputeValue(InstantPercentageSample sample)
        {
            sample.ThrowIfNull("sample");
            if(sample.Denominator == 0)
            {
                return 0.0F;
            }
            return (float) sample.Numerator / sample.Denominator * 100.0F;
        }

        // Average Meters

        public static float ComputeValue(AverageValueSample sample0, AverageValueSample sample1)
        {
            sample0.ThrowIfNull("sample0");
            sample1.ThrowIfNull("sample1");
            if(sample0.TimeStamp > sample1.TimeStamp && sample0.Count != sample1.Count)
            {
                return (float) (sample0.Value - sample1.Value) / (sample0.Count - sample1.Count);
            }
            if(sample1.TimeStamp > sample0.TimeStamp && sample1.Count != sample0.Count)
            {
                return (float) (sample1.Value - sample0.Value) / (sample1.Count - sample0.Count);
            }
            return 0.0F;
        }

        public static float ComputeValue(AverageTimeSample sample0, AverageTimeSample sample1)
        {
            sample0.ThrowIfNull("sample0");
            sample1.ThrowIfNull("sample1");
            if(sample0.TimeStamp > sample1.TimeStamp && sample0.Count != sample1.Count)
            {
                return (sample0.ElapsedTime - sample1.ElapsedTime).Seconds / (sample0.Count - sample1.Count);
            }
            if(sample1.TimeStamp > sample0.TimeStamp && sample1.Count != sample0.Count)
            {
                return (sample1.ElapsedTime - sample0.ElapsedTime).Seconds / (sample1.Count - sample0.Count);
            }
            return 0.0F;
        }

        public static float ComputeValue(SampleRateSample sample0, SampleRateSample sample1)
        {
            sample0.ThrowIfNull("sample0");
            sample1.ThrowIfNull("sample1");
            if(sample0.TimeStamp > sample1.TimeStamp)
            {
                return (sample0.Count - sample1.Count) / (sample0.TimeStamp - sample1.TimeStamp).Seconds;
            }
            if(sample1.TimeStamp > sample0.TimeStamp)
            {
                return (sample1.Count - sample0.Count) / (sample1.TimeStamp - sample0.TimeStamp).Seconds;
            }
            return 0.0F;
        }

        public static float ComputeValue(SamplePercentageSample sample0, SamplePercentageSample sample1)
        {
            sample0.ThrowIfNull("sample0");
            sample1.ThrowIfNull("sample1");
            if(sample0.TimeStamp > sample1.TimeStamp)
            {
                return (float) (sample0.Numerator - sample1.Numerator) / (sample0.Denominator - sample1.Denominator) * 100.0F;
            }
            if(sample1.TimeStamp > sample0.TimeStamp)
            {
                return (float) (sample1.Numerator - sample0.Numerator) / (sample1.Denominator - sample0.Denominator) * 100.0F;
            }
            return 0.0F;
        }

        // Difference Counters

        public static long ComputeValue(DifferentialValueSample sample0, DifferentialValueSample sample1)
        {
            sample0.ThrowIfNull("sample0");
            sample1.ThrowIfNull("sample1");
            if(sample0.TimeStamp > sample1.TimeStamp)
            {
                return sample0.Value - sample1.Value;
            }
            if(sample1.TimeStamp > sample0.TimeStamp)
            {
                return sample1.Value - sample0.Value;
            }
            return 0L;
        }

        // Percentage Counters

        public static float ComputeValue(TimerSample sample0, TimerSample sample1)
        {
            sample0.ThrowIfNull("sample0");
            sample1.ThrowIfNull("sample1");
            if(sample0.TimeStamp > sample1.TimeStamp)
            {
                return (float) (sample0.ElapsedTimeOfActivity - sample1.ElapsedTimeOfActivity).Ticks / (sample0.TimeStamp - sample1.TimeStamp).Ticks * 100.0F;
            }
            if(sample1.TimeStamp > sample0.TimeStamp)
            {
                return (float) (sample1.ElapsedTimeOfActivity - sample0.ElapsedTimeOfActivity).Ticks / (sample1.TimeStamp - sample0.TimeStamp).Ticks * 100.0F;
            }
            return 0.0F;
        }

        public static float ComputeValue(TimerInverseSample sample0, TimerInverseSample sample1)
        {
            sample0.ThrowIfNull("sample0");
            sample1.ThrowIfNull("sample1");
            if(sample0.TimeStamp > sample1.TimeStamp)
            {
                return (1.0F - ((float) (sample0.ElapsedTimeOfInactivity - sample1.ElapsedTimeOfInactivity).Ticks / (sample0.TimeStamp - sample1.TimeStamp).Ticks)) * 100.0F;
            }
            if(sample1.TimeStamp > sample0.TimeStamp)
            {
                return (1.0F - ((float) (sample1.ElapsedTimeOfInactivity - sample0.ElapsedTimeOfInactivity).Ticks / (sample1.TimeStamp - sample0.TimeStamp).Ticks)) * 100.0F;
            }
            return 0.0F;
        }

        public static float ComputeValue(Timer100NsSample sample0, Timer100NsSample sample1)
        {
            sample0.ThrowIfNull("sample0");
            sample1.ThrowIfNull("sample1");
            if(sample0.TimeStamp100Ns > sample1.TimeStamp100Ns)
            {
                return (float) (sample0.ElapsedTimeOfActivity - sample1.ElapsedTimeOfActivity).Ticks / (sample0.TimeStamp100Ns - sample1.TimeStamp100Ns).Ticks * 100.0F;
            }
            if(sample1.TimeStamp100Ns > sample0.TimeStamp100Ns)
            {
                return (float) (sample1.ElapsedTimeOfActivity - sample0.ElapsedTimeOfActivity).Ticks / (sample1.TimeStamp100Ns - sample0.TimeStamp100Ns).Ticks * 100.0F;
            }
            return 0.0F;
        }

        public static float ComputeValue(Timer100NsInverseSample sample0, Timer100NsInverseSample sample1)
        {
            sample0.ThrowIfNull("sample0");
            sample1.ThrowIfNull("sample1");
            if(sample0.TimeStamp100Ns > sample1.TimeStamp100Ns)
            {
                return (1.0F - ((float) (sample0.ElapsedTimeOfInactivity - sample1.ElapsedTimeOfInactivity).Ticks / (sample0.TimeStamp100Ns - sample1.TimeStamp100Ns).Ticks)) * 100.0F;
            }
            if(sample1.TimeStamp100Ns > sample0.TimeStamp100Ns)
            {
                return (1.0F - ((float) (sample1.ElapsedTimeOfInactivity - sample0.ElapsedTimeOfInactivity).Ticks / (sample1.TimeStamp100Ns - sample0.TimeStamp100Ns).Ticks)) * 100.0F;
            }
            return 0.0F;
        }

        public static float ComputeValue(MultiTimerSample sample0, MultiTimerSample sample1)
        {
            sample0.ThrowIfNull("sample0");
            sample1.ThrowIfNull("sample1");
            if(sample0.TimeStamp > sample1.TimeStamp)
            {
                return (float) (sample0.ElapsedTimeOfActivity - sample1.ElapsedTimeOfActivity).Ticks / (sample0.TimeStamp - sample1.TimeStamp).Ticks * 100.0F / (sample0.Count - sample1.Count);
            }
            if(sample1.TimeStamp > sample0.TimeStamp)
            {
                return (float) (sample1.ElapsedTimeOfActivity - sample0.ElapsedTimeOfActivity).Ticks / (sample1.TimeStamp - sample0.TimeStamp).Ticks * 100.0F / (sample1.Count - sample0.Count);
            }
            return 0.0F;
        }

        public static float ComputeValue(MultiTimerInverseSample sample0, MultiTimerInverseSample sample1)
        {
            sample0.ThrowIfNull("sample0");
            sample1.ThrowIfNull("sample1");
            if(sample0.TimeStamp > sample1.TimeStamp)
            {
                return ((sample0.Count - sample1.Count) - ((float) (sample0.ElapsedTimeOfInactivity - sample1.ElapsedTimeOfInactivity).Ticks / (sample0.TimeStamp - sample1.TimeStamp).Ticks)) * 100.0F;
            }
            if(sample1.TimeStamp > sample0.TimeStamp)
            {
                return ((sample1.Count - sample0.Count) - ((float) (sample1.ElapsedTimeOfInactivity - sample0.ElapsedTimeOfInactivity).Ticks / (sample1.TimeStamp - sample0.TimeStamp).Ticks)) * 100.0F;
            }
            return 0.0F;
        }

        public static float ComputeValue(MultiTimer100NsSample sample0, MultiTimer100NsSample sample1)
        {
            sample0.ThrowIfNull("sample0");
            sample1.ThrowIfNull("sample1");
            if(sample0.TimeStamp100Ns > sample1.TimeStamp100Ns)
            {
                return (float) (sample0.ElapsedTimeOfActivity - sample1.ElapsedTimeOfActivity).Ticks / (sample0.TimeStamp100Ns - sample1.TimeStamp100Ns).Ticks * 100.0F / (sample0.Count - sample1.Count);
            }
            if(sample1.TimeStamp100Ns > sample0.TimeStamp100Ns)
            {
                return (float) (sample1.ElapsedTimeOfActivity - sample0.ElapsedTimeOfActivity).Ticks / (sample1.TimeStamp100Ns - sample0.TimeStamp100Ns).Ticks * 100.0F / (sample1.Count - sample0.Count);
            }
            return 0.0F;
        }

        public static float ComputeValue(MultiTimer100NsInverseSample sample0, MultiTimer100NsInverseSample sample1)
        {
            sample0.ThrowIfNull("sample0");
            sample1.ThrowIfNull("sample1");
            if(sample0.TimeStamp100Ns > sample1.TimeStamp100Ns)
            {
                return ((sample0.Count - sample1.Count) - ((float) (sample0.ElapsedTimeOfInactivity - sample1.ElapsedTimeOfInactivity).Ticks / (sample0.TimeStamp100Ns - sample1.TimeStamp100Ns).Ticks)) * 100.0F;
            }
            if(sample1.TimeStamp100Ns > sample0.TimeStamp100Ns)
            {
                return ((sample1.Count - sample0.Count) - ((float) (sample1.ElapsedTimeOfInactivity - sample0.ElapsedTimeOfInactivity).Ticks / (sample1.TimeStamp100Ns - sample0.TimeStamp100Ns).Ticks)) * 100.0F;
            }
            return 0.0F;
        }

        #endregion
    }
}
