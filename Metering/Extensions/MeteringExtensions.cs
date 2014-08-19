using System;
using System.Diagnostics;
using System.Linq;
using NDiagnostics.Metering.Attributes;

namespace NDiagnostics.Metering.Extensions
{
    internal static class MeteringExtensions
    {
        #region Methods

        internal static MeterCategoryAttribute GetMeterCategoryAttribute(this Type enumType)
        {
            return Attribute.GetCustomAttribute(enumType, typeof(MeterCategoryAttribute)) as MeterCategoryAttribute;
        }

        internal static MeterAttribute GetMeterAttribute(this Type enumType, object enumValue)
        {
            if(enumType.IsNotNull())
            {
                var fieldInfo = enumType.GetField(enumValue.ToString());
                if(fieldInfo.IsNotNull())
                {
                    var attributes = fieldInfo.GetCustomAttributes(typeof(MeterAttribute), false) as MeterAttribute[];
                    return attributes.FirstOrDefault();
                }
            }
            return null;
        }

        internal static PerformanceCounterType? GetPerformanceCounterType(this MeterAttribute meterAttribute)
        {
            switch(meterAttribute.MeterType)
            {
                case MeterType.InstantValue:
                    return meterAttribute.DataType == MeterDataType.Int32 ? PerformanceCounterType.NumberOfItems32 : PerformanceCounterType.NumberOfItems64;
                case MeterType.InstantPercentage:
                    return PerformanceCounterType.RawFraction;
                case MeterType.AverageValue:
                    return PerformanceCounterType.AverageCount64;
                case MeterType.AverageTime:
                    return PerformanceCounterType.AverageTimer32;
                case MeterType.DifferentialValue:
                    return meterAttribute.DataType == MeterDataType.Int32 ? PerformanceCounterType.CounterDelta32 : PerformanceCounterType.CounterDelta64;
                case MeterType.InstantTime:
                    return PerformanceCounterType.ElapsedTime;
                case MeterType.SampleRate:
                    return meterAttribute.DataType == MeterDataType.Int32 ? PerformanceCounterType.RateOfCountsPerSecond32 : PerformanceCounterType.RateOfCountsPerSecond64;
                case MeterType.SamplePercentage:
                    return PerformanceCounterType.SampleFraction;
                case MeterType.Timer:
                    return PerformanceCounterType.CounterTimer;
                case MeterType.TimerInverse:
                    return PerformanceCounterType.CounterTimerInverse;
                case MeterType.MultiTimer:
                    return PerformanceCounterType.CounterMultiTimer;
                case MeterType.MultiTimerInverse:
                    return PerformanceCounterType.CounterMultiTimerInverse;
                case MeterType.Timer100Ns:
                    return PerformanceCounterType.Timer100Ns;
                case MeterType.Timer100NsInverse:
                    return PerformanceCounterType.Timer100NsInverse;
                case MeterType.MultiTimer100Ns:
                    return PerformanceCounterType.CounterMultiTimer100Ns;
                case MeterType.MultiTimer100NsInverse:
                    return PerformanceCounterType.CounterMultiTimer100NsInverse;
            }
            return null;
        }

        internal static PerformanceCounterType? GetBaseType(this PerformanceCounterType type)
        {
            switch(type)
            {
                case PerformanceCounterType.SampleFraction:
                case PerformanceCounterType.SampleCounter:
                    return PerformanceCounterType.SampleBase;

                case PerformanceCounterType.CounterMultiTimer:
                case PerformanceCounterType.CounterMultiTimer100NsInverse:
                case PerformanceCounterType.CounterMultiTimer100Ns:
                case PerformanceCounterType.CounterMultiTimerInverse:
                    return PerformanceCounterType.CounterMultiBase;

                case PerformanceCounterType.RawFraction:
                    return PerformanceCounterType.RawBase;

                case PerformanceCounterType.AverageTimer32:
                case PerformanceCounterType.AverageCount64:
                    return PerformanceCounterType.AverageBase;
            }
            return null;
        }

        internal static string GetNameForBaseType(this string name)
        {
            return name + "Base";
        }

        #endregion
    }
}
