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

        public static IMeterCategory<T> Create<T>(IEnumerable<string> instanceNames)
        {
            var typeT = typeof(T).ThrowIfNotEnum();

            var meterCategoryAttribute = typeT.GetMeterCategoryAttribute()
                .ThrowIfNull(new NotSupportedException(string.Format("Enum '{0}' must be decorated by a MeterCategory attribute.", typeT.ToName()), null));

            var values = Enum.GetValues(typeT)
                .ThrowIfEmpty(new NotSupportedException(string.Format("Enum '{0}' must contain at least one value.", typeT.ToName()), null));

            var meterAttributes = new Dictionary<T, MeterAttribute>();
            foreach(T value in values)
            {
                var meterAttribute = typeT.GetMeterAttribute(value)
                    .ThrowIfNull(new NotSupportedException(string.Format("Value '{0}' of enum '{1}' must de decorated by a Meter attribute.", value, typeT.ToName()), null));

                meterAttributes.Add(value, meterAttribute);
            }

            if (meterCategoryAttribute.MeterCategoryType == MeterCategoryType.SingleInstance && instanceNames != null)
            {
                throw new NotSupportedException(string.Format("Enum '{0}' is of type 'SingleInstance' and must be created without an instance name.", typeT.ToName()), null);
            }

            return new MeterCategory<T>(meterCategoryAttribute, meterAttributes, instanceNames);
        }

        public static void Install<T>()
        {
            var typeT = typeof(T).ThrowIfNotEnum();

            var meterCategoryAttribute = typeT.GetMeterCategoryAttribute()
                .ThrowIfNull(new NotSupportedException(string.Format("Enum '{0}' must be decorated by a MeterCategory attribute.", typeT.ToName()), null));

            var values = Enum.GetValues(typeT)
                .ThrowIfEmpty(new NotSupportedException(string.Format("Enum '{0}' must contain at least one value.", typeT.ToName()), null));

            var meterAttributes = new List<MeterAttribute>();
            foreach(T value in values)
            {
                var meterAttribute = typeT.GetMeterAttribute(value)
                    .ThrowIfNull(new NotSupportedException(string.Format("Value '{0}' of enum '{1}' must de decorated by a Meter attribute.", value, typeT.ToName()), null));

                meterAttributes.Add(meterAttribute);
            }

            if(PerformanceCounterCategory.Exists(meterCategoryAttribute.Name))
            {
                PerformanceCounterCategory.Delete(meterCategoryAttribute.Name);
            }

            var counterCreationDataCollection = new CounterCreationDataCollection();
            foreach(var meterAttribute in meterAttributes)
            {
                var counterType = meterAttribute.GetPerformanceCounterType();
                if(counterType.HasValue)
                {
                    var counterCreationData = new CounterCreationData(meterAttribute.Name, meterAttribute.Description, counterType.Value);
                    counterCreationDataCollection.Add(counterCreationData);
                    var baseType = counterCreationData.CounterType.GetBaseType();
                    if(baseType.HasValue)
                    {
                        counterCreationDataCollection.Add(new CounterCreationData(meterAttribute.Name.GetNameForBaseType(), string.Format("Base for {0}", meterAttribute.Name), baseType.Value));
                    }
                }
            }

            PerformanceCounterCategory.Create(meterCategoryAttribute.Name, meterCategoryAttribute.Description, (PerformanceCounterCategoryType) meterCategoryAttribute.MeterCategoryType, counterCreationDataCollection);
        }

        public static void Uninstall<T>()
        {
            var typeT = typeof(T).ThrowIfNotEnum();

            var meterCategoryAttribute = typeT.GetMeterCategoryAttribute()
                .ThrowIfNull(new NotSupportedException(string.Format("Enum '{0}' must be decorated by a MeterCategory attribute.", typeT.ToName()), null));

            var values = Enum.GetValues(typeT)
                .ThrowIfEmpty(new NotSupportedException(string.Format("Enum '{0}' must contain at least one value.", typeT.ToName()), null));

            foreach (T value in values)
            {
                typeT.GetMeterAttribute(value)
                    .ThrowIfNull(new NotSupportedException(string.Format("Value '{0}' of enum '{1}' must de decorated by a Meter attribute.", value, typeT.ToName()), null));
            }

            if (PerformanceCounterCategory.Exists(meterCategoryAttribute.Name))
            {
                PerformanceCounterCategory.Delete(meterCategoryAttribute.Name);
            }
        }

        [DebuggerStepThrough]
        public static T Cast<T>(this IMeter obj) where T : class, IMeter
        {
            return obj == null ? null : obj as T;
        }

        #endregion
    }

    internal sealed class MeterCategory<T> : DisposableObject, IMeterCategory<T>
    {
        #region Constants and Fields

        private readonly IDictionary<string, IDictionary<T, IMeter>> meters;

        private readonly IDictionary<T, MeterAttribute> meterAttributes;

        #endregion

        #region Constructors and Destructors

        internal MeterCategory(MeterCategoryAttribute meterCategoryAttribute, IDictionary<T, MeterAttribute> meterAttributes, IEnumerable<string> instanceNames)
        {
            this.meters = new Dictionary<string, IDictionary<T, IMeter>>();
            this.CategoryName = meterCategoryAttribute.Name;
            this.CategoryType = meterCategoryAttribute.MeterCategoryType;
            this.meterAttributes = meterAttributes;

            if(meterCategoryAttribute.MeterCategoryType == MeterCategoryType.SingleInstance)
            {
                var instanceMeters = CreateInstanceMeters(meterCategoryAttribute.Name, meterCategoryAttribute.MeterCategoryType, meterAttributes, SingleInstance.DefaultInstanceName);
                this.meters.Add(SingleInstance.DefaultInstanceName, instanceMeters);
            }
            else
            {
                if(instanceNames == null)
                {
                    var instanceMeters = CreateInstanceMeters(meterCategoryAttribute.Name, meterCategoryAttribute.MeterCategoryType, meterAttributes, MultiInstance.DefaultInstanceName);
                    this.meters.Add(MultiInstance.DefaultInstanceName, instanceMeters);
                }
                else
                {
                    foreach(var instanceName in instanceNames)
                    {
                        var instanceMeters = CreateInstanceMeters(meterCategoryAttribute.Name, meterCategoryAttribute.MeterCategoryType, meterAttributes, instanceName);
                        this.meters.Add(instanceName, instanceMeters);
                    }
                }
            }
        }

        #endregion

        #region IMeterCategory

        public string CategoryName { get; private set; }

        public MeterCategoryType CategoryType { get; private set; }

        public string[] InstanceNames
        {
            get { return this.meters.Keys.ToArray(); }
        }

        #endregion

        #region IMeterCategory<T>

        public IMeter this[T meterName]
        {
            get { return this.GetMeter(meterName); }
        }

        public IMeter this[T meterName, string instanceName]
        {
            get { return this.GetMeter(meterName, instanceName); }
        }

        public void CreateInstance(string instanceName, InstanceLifetime instanceLifetime = InstanceLifetime.Process)
        {
            if (this.CategoryType == MeterCategoryType.SingleInstance)
            {
                throw new InvalidOperationException(string.Format("Instances cannot be created on meter categories of type 'SingleInstance'."), null);
            }

            var instanceMeters = CreateInstanceMeters(this.CategoryName, this.CategoryType, this.meterAttributes, instanceName, instanceLifetime);
            this.meters.Add(instanceName, instanceMeters);
        }

        #endregion

        #region Methods

        protected override void OnDisposing()
        {
            if((this.meters != null) && (this.meters.Count > 0))
            {
                var instanceMeters = this.meters.Values.ToList();
                this.meters.Clear();

                foreach(var instanceMeter in instanceMeters)
                {
                    if(instanceMeter != null)
                    {
                        foreach(var meter in instanceMeter.Values)
                        {
                            meter.Dispose();
                        }
                    }
                }
            }
        }

        private static IDictionary<T, IMeter> CreateInstanceMeters(string meterCategoryName, MeterCategoryType meterCategoryType, IEnumerable<KeyValuePair<T, MeterAttribute>> meterAttributes, string instanceName, InstanceLifetime instanceLifetime = InstanceLifetime.Global)
        {
            var instanceMeters = new Dictionary<T, IMeter>();

            var enumerator = meterAttributes.GetEnumerator();
            while(enumerator.MoveNext())
            {
                var meterAttribute = enumerator.Current.Value;

                var meter = CreateMeter(meterCategoryName, meterCategoryType, meterAttribute.Name, meterAttribute.MeterType, instanceName, instanceLifetime);

                instanceMeters.Add(enumerator.Current.Key, meter);
            }
            return instanceMeters;
        }

        private static IMeter CreateMeter(string meterCategoryName, MeterCategoryType meterCategoryType, string meterName, MeterType meterType, string instanceName, InstanceLifetime instanceLifetime)
        {
            IMeter meter = null;
            switch(meterType)
            {
                    // Instant Meters
                case MeterType.InstantValue:
                    meter = new InstantValueMeter(meterCategoryName, meterCategoryType, meterName, meterType, instanceName, instanceLifetime);
                    break;
                case MeterType.InstantPercentage:
                    meter = new InstantPercentageMeter(meterCategoryName, meterCategoryType, meterName, meterType, instanceName, instanceLifetime);
                    break;
                    // Average Meters
                case MeterType.AverageValue:
                    meter = new AverageValueMeter(meterCategoryName, meterCategoryType, meterName, meterType, instanceName, instanceLifetime);
                    break;
                case MeterType.AverageTime:
                    meter = new AverageTimeMeter(meterCategoryName, meterCategoryType, meterName, meterType, instanceName, instanceLifetime);
                    break;
                    // Difference Meters
                case MeterType.DifferentialValue:
                    meter = new DifferentialValueMeter(meterCategoryName, meterCategoryType, meterName, meterType, instanceName, instanceLifetime);
                    break;
                case MeterType.InstantTime:
                    meter = new InstantTimeMeter(meterCategoryName, meterCategoryType, meterName, meterType, instanceName, instanceLifetime);
                    break;
                case MeterType.SampleRate:
                    meter = new SampleRateMeter(meterCategoryName, meterCategoryType, meterName, meterType, instanceName, instanceLifetime);
                    break;
                case MeterType.SamplePercentage:
                    meter = new SamplePercentageMeter(meterCategoryName, meterCategoryType, meterName, meterType, instanceName, instanceLifetime);
                    break;
                case MeterType.Timer:
                    meter = new TimerMeter(meterCategoryName, meterCategoryType, meterName, meterType, instanceName, instanceLifetime);
                    break;
                case MeterType.TimerInverse:
                    meter = new TimerInverseMeter(meterCategoryName, meterCategoryType, meterName, meterType, instanceName, instanceLifetime);
                    break;
                case MeterType.Timer100Ns:
                    meter = new Timer100nsMeter(meterCategoryName, meterCategoryType, meterName, meterType, instanceName, instanceLifetime);
                    break;
                case MeterType.Timer100NsInverse:
                    meter = new Timer100nsInverseMeter(meterCategoryName, meterCategoryType, meterName, meterType, instanceName, instanceLifetime);
                    break;
                case MeterType.MultiTimer:
                    meter = new MultiTimerMeter(meterCategoryName, meterCategoryType, meterName, meterType, instanceName, instanceLifetime);
                    break;
                case MeterType.MultiTimerInverse:
                    meter = new MultiTimerInverseMeter(meterCategoryName, meterCategoryType, meterName, meterType, instanceName, instanceLifetime);
                    break;
                case MeterType.MultiTimer100Ns:
                    meter = new MultiTimer100NsMeter(meterCategoryName, meterCategoryType, meterName, meterType, instanceName, instanceLifetime);
                    break;
                case MeterType.MultiTimer100NsInverse:
                    meter = new MultiTimer100NsInverseMeter(meterCategoryName, meterCategoryType, meterName, meterType, instanceName, instanceLifetime);
                    break;
            }
            return meter;
        }

        private IMeter GetMeter(T meterName, string instanceName = null)
        {
            instanceName = instanceName ?? (this.CategoryType == MeterCategoryType.SingleInstance ? SingleInstance.DefaultInstanceName : MultiInstance.DefaultInstanceName);
            if(this.meters.ContainsKey(instanceName) && this.meters[instanceName].ContainsKey(meterName))
            {
                return this.meters[instanceName][meterName];
            }
            return null;
        }

        #endregion
    }
}
