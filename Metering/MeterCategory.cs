using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NDiagnostics.Metering.Attributes;
using NDiagnostics.Metering.Extensions;
using NDiagnostics.Metering.Meters;

namespace NDiagnostics.Metering
{
    public static class MeterCategory
    {
        #region Public Methods

        public static IMeterCategory<T> Create<T>()
        {
            return Create<T>(null);
        }

        public static IMeterCategory<T> Create<T>(string[] instanceNames)
        {
            var typeT = typeof(T);
            if(!typeT.IsEnum)
            {
                throw new NotSupportedException(string.Format("{0} must be an enum type.", typeT.Name));
            }

            var meterCategoryAttribute = typeT.GetMeterCategoryAttribute();
            if(meterCategoryAttribute == null)
            {
                throw new NotSupportedException(string.Format("Enum '{0}' must have a MeterCategoryAttribute associated.", typeT.Name));
            }

            var enumValues = Enum.GetValues(typeT);

            var meterAttributes = new Dictionary<T, MeterAttribute>();
            foreach(T meter in enumValues)
            {
                var attr = typeT.GetMeterAttribute(meter);
                if(attr != null)
                {
                    meterAttributes.Add(meter, attr);
                }
            }

            if(meterCategoryAttribute.Type == MeterCategoryType.MultiInstance)
            {
                return new MeterCategory<T>(meterCategoryAttribute, meterAttributes, instanceNames);
            }

            return new MeterCategory<T>(meterCategoryAttribute, meterAttributes);
        }

        public static void Install<T>()
        {
            var typeT = typeof(T);
            if(!typeT.IsEnum)
            {
                throw new NotSupportedException(string.Format("{0} must be an enum type.", typeT.Name));
            }

            var meterCategoryAttribute = typeT.GetMeterCategoryAttribute();
            if(meterCategoryAttribute == null)
            {
                throw new NotSupportedException(string.Format("Enum '{0}' must have a MeterCategoryAttribute associated.", typeT.Name));
            }

            var enumValues = Enum.GetValues(typeT);

            if(PerformanceCounterCategory.Exists(meterCategoryAttribute.Name))
            {
                PerformanceCounterCategory.Delete(meterCategoryAttribute.Name);
            }

            var categoryCounters = new CounterCreationDataCollection();

            foreach(var performanceCounter in enumValues)
            {
                var meterAttribute = typeT.GetMeterAttribute(performanceCounter);
                if(meterAttribute != null)
                {
                    var counterType = meterAttribute.GetPerformanceCounterType();
                    if(counterType.HasValue)
                    {
                        var counterData = new CounterCreationData(meterAttribute.Name, meterAttribute.Description, counterType.Value);
                        categoryCounters.Add(counterData);
                        var baseType = counterData.CounterType.GetBaseType();
                        if(baseType.HasValue)
                        {
                            categoryCounters.Add(new CounterCreationData(meterAttribute.Name.GetNameForBaseType(), string.Format("Base for {0}", meterAttribute.Name), baseType.Value));
                        }
                    }
                }
            }

            if(categoryCounters.Count > 0)
            {
                PerformanceCounterCategory.Create(meterCategoryAttribute.Name, meterCategoryAttribute.Description, (PerformanceCounterCategoryType) meterCategoryAttribute.Type, categoryCounters);
            }
        }

        public static void Uninstall<T>()
        {
            var typeT = typeof(T);
            if(!typeT.IsEnum)
            {
                throw new NotSupportedException(string.Format("{0} must be an enum type.", typeT.Name));
            }

            var meterCategoryAttribute = typeT.GetMeterCategoryAttribute();
            if(meterCategoryAttribute == null)
            {
                throw new NotSupportedException(string.Format("Enum '{0}' must have a MeterCategoryAttribute associated.", typeT.Name));
            }

            if(PerformanceCounterCategory.Exists(meterCategoryAttribute.Name))
            {
                PerformanceCounterCategory.Delete(meterCategoryAttribute.Name);
            }
        }

        public static T Cast<T>(this IMeter obj) where T : class, IMeter
        {
            return obj == null ? null : obj as T;
        }

        #endregion
    }

    internal class MeterCategory<T> : DisposableObject, IMeterCategory<T>
    {
        #region Constants and Fields

        private readonly Dictionary<string, Dictionary<T, IMeter>> meters;

        #endregion

        #region Constructors and Destructors

        internal MeterCategory(MeterCategoryAttribute meterCategoryAttribute, Dictionary<T, MeterAttribute> meterAttributes, string[] instanceNames = null)
        {
            meterCategoryAttribute.ThrowIfNull("meterCategoryAttribute");
            meterAttributes.ThrowIfNull("meterAttributes");

            instanceNames = instanceNames ?? new[] {string.Empty};
            for(var i = 0; i < instanceNames.Length; i++)
            {
                instanceNames[i].ThrowIfExceedsMaxSize(string.Format("instanceNames[{0}]", i), 127);
            }

            if(meterCategoryAttribute.Type == MeterCategoryType.MultiInstance && instanceNames.Any(instanceName => instanceName == null) && instanceNames.Count() != instanceNames.Distinct().Count())
            {
                throw new NotSupportedException("Meter categories of type 'MultiInstance' must have uniquely named instances.");
            }

            this.meters = new Dictionary<string, Dictionary<T, IMeter>>();
            this.CategoryName = meterCategoryAttribute.Name;
            this.CategoryType = meterCategoryAttribute.Type;
            this.InstanceNames = instanceNames;

            foreach(var instanceName in instanceNames)
            {
                var instanceMeters = new Dictionary<T, IMeter>();

                var enumerator = meterAttributes.GetEnumerator();
                while(enumerator.MoveNext())
                {
                    var meterAttribute = enumerator.Current.Value;

                    IMeter meter = null;
                    switch(meterAttribute.MeterType)
                    {
                            // Instant Meters
                        case MeterType.InstantValue:
                            meter = new InstantValueMeter(meterCategoryAttribute.Name, meterCategoryAttribute.Type, meterAttribute.Name, meterAttribute.MeterType, instanceName);
                            break;
                        case MeterType.InstantRatio:
                            meter = new InstantRatioMeter(meterCategoryAttribute.Name, meterCategoryAttribute.Type, meterAttribute.Name, meterAttribute.MeterType, instanceName);
                            break;
                            // Average Meters
                        case MeterType.AverageValue:
                            meter = new AverageValueMeter(meterCategoryAttribute.Name, meterCategoryAttribute.Type, meterAttribute.Name, meterAttribute.MeterType, instanceName);
                            break;
                        case MeterType.AverageTime:
                            meter = new AverageTimeMeter(meterCategoryAttribute.Name, meterCategoryAttribute.Type, meterAttribute.Name, meterAttribute.MeterType, instanceName);
                            break;
                            // Difference Meters
                        case MeterType.DifferentialValue:
                            meter = new DifferentialValueMeter(meterCategoryAttribute.Name, meterCategoryAttribute.Type, meterAttribute.Name, meterAttribute.MeterType, instanceName);
                            break;
                        case MeterType.InstantTime:
                            meter = new InstantTimeMeter(meterCategoryAttribute.Name, meterCategoryAttribute.Type, meterAttribute.Name, meterAttribute.MeterType, instanceName);
                            break;
                        case MeterType.AverageRate:
                            meter = new AverageRateMeter(meterCategoryAttribute.Name, meterCategoryAttribute.Type, meterAttribute.Name, meterAttribute.MeterType, instanceName);
                            break;
                        case MeterType.AverageRatio:
                            meter = new AverageRatioMeter(meterCategoryAttribute.Name, meterCategoryAttribute.Type, meterAttribute.Name, meterAttribute.MeterType, instanceName);
                            break;
                        case MeterType.Timer:
                            meter = new TimerMeter(meterCategoryAttribute.Name, meterCategoryAttribute.Type, meterAttribute.Name, meterAttribute.MeterType, instanceName);
                            break;
                        case MeterType.TimerInverse:
                            meter = new TimerInverseMeter(meterCategoryAttribute.Name, meterCategoryAttribute.Type, meterAttribute.Name, meterAttribute.MeterType, instanceName);
                            break;
                        case MeterType.Timer100Ns:
                            meter = new Timer100nsMeter(meterCategoryAttribute.Name, meterCategoryAttribute.Type, meterAttribute.Name, meterAttribute.MeterType, instanceName);
                            break;
                        case MeterType.Timer100NsInverse:
                            meter = new Timer100nsInverseMeter(meterCategoryAttribute.Name, meterCategoryAttribute.Type, meterAttribute.Name, meterAttribute.MeterType, instanceName);
                            break;
                        case MeterType.MultiTimer:
                            meter = new MultiTimerMeter(meterCategoryAttribute.Name, meterCategoryAttribute.Type, meterAttribute.Name, meterAttribute.MeterType, instanceName);
                            break;
                        case MeterType.MultiTimerInverse:
                            meter = new MultiTimerInverseMeter(meterCategoryAttribute.Name, meterCategoryAttribute.Type, meterAttribute.Name, meterAttribute.MeterType, instanceName);
                            break;
                        case MeterType.MultiTimer100Ns:
                            meter = new MultiTimer100NsMeter(meterCategoryAttribute.Name, meterCategoryAttribute.Type, meterAttribute.Name, meterAttribute.MeterType, instanceName);
                            break;
                        case MeterType.MultiTimer100NsInverse:
                            meter = new MultiTimer100NsInverseMeter(meterCategoryAttribute.Name, meterCategoryAttribute.Type, meterAttribute.Name, meterAttribute.MeterType, instanceName);
                            break;
                    }
                    instanceMeters.Add(enumerator.Current.Key, meter);
                }
                this.meters.Add(instanceName, instanceMeters);
            }
        }

        #endregion

        #region IMeterCategory

        public string CategoryName { get; private set; }

        public MeterCategoryType CategoryType { get; private set; }

        public string[] InstanceNames { get; private set; }

        #endregion

        #region IMeterCategory<T>

        public IMeter this[T meterName]
        {
            get { return this.meters[string.Empty][meterName]; }
        }

        public IMeter this[T meterName, string instanceName]
        {
            get { return this.meters[instanceName][meterName]; }
        }

        #endregion

        #region Methods

        protected override void InternalDispose()
        {
            if((this.meters != null) && (this.meters.Count > 0))
            {
                var instances = this.meters.Values.ToArray();
                this.meters.Clear();

                foreach(var instanceMeters in instances)
                {
                    if(instanceMeters != null)
                    {
                        foreach(var meter in instanceMeters.Values)
                        {
                            meter.TryDispose();
                        }
                    }
                }
            }
        }

        #endregion
    }
}
