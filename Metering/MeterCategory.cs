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
            var typeT = typeof(T).ThrowIfNotEnum();

            var meterCategoryAttribute = typeT.GetMeterCategoryAttribute()
                .ThrowIfNull(new NotSupportedException($"Enum '{typeT.ToName()}' must be decorated by a MeterCategory attribute.", null));

            var values = Enum.GetValues(typeT)
                .ThrowIfEmpty(new NotSupportedException($"Enum '{typeT.ToName()}' must contain at least one value.", null));

            var meterAttributes = new Dictionary<T, MeterAttribute>();
            foreach(T value in values)
            {
                var meterAttribute = typeT.GetMeterAttribute(value)
                    .ThrowIfNull(new NotSupportedException($"Value '{value}' of enum '{typeT.ToName()}' must de decorated by a Meter attribute.", null));

                meterAttributes.Add(value, meterAttribute);
            }

            return new MeterCategory<T>(meterCategoryAttribute, meterAttributes);
        }

        public static void Install<T>()
        {
            var typeT = typeof(T).ThrowIfNotEnum();

            var meterCategoryAttribute = typeT.GetMeterCategoryAttribute()
                .ThrowIfNull(new NotSupportedException($"Enum '{typeT.ToName()}' must be decorated by a MeterCategory attribute.", null));

            var values = Enum.GetValues(typeT)
                .ThrowIfEmpty(new NotSupportedException($"Enum '{typeT.ToName()}' must contain at least one value.", null));

            var meterAttributes = new List<MeterAttribute>();
            foreach(T value in values)
            {
                var meterAttribute = typeT.GetMeterAttribute(value)
                    .ThrowIfNull(new NotSupportedException($"Value '{value}' of enum '{typeT.ToName()}' must de decorated by a Meter attribute.", null));

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
                        counterCreationDataCollection.Add(new CounterCreationData(meterAttribute.Name.GetNameForBaseType(), $"Base for {meterAttribute.Name}", baseType.Value));
                    }
                }
            }

            PerformanceCounterCategory.Create(meterCategoryAttribute.Name, meterCategoryAttribute.Description, (PerformanceCounterCategoryType) meterCategoryAttribute.MeterCategoryType, counterCreationDataCollection);
        }

        public static void Uninstall<T>()
        {
            var typeT = typeof(T).ThrowIfNotEnum();

            var meterCategoryAttribute = typeT.GetMeterCategoryAttribute()
                .ThrowIfNull(new NotSupportedException($"Enum '{typeT.ToName()}' must be decorated by a MeterCategory attribute.", null));

            var values = Enum.GetValues(typeT)
                .ThrowIfEmpty(new NotSupportedException($"Enum '{typeT.ToName()}' must contain at least one value.", null));

            foreach (T value in values)
            {
                typeT.GetMeterAttribute(value)
                    .ThrowIfNull(new NotSupportedException($"Value '{value}' of enum '{typeT.ToName()}' must de decorated by a Meter attribute.", null));
            }

            if (PerformanceCounterCategory.Exists(meterCategoryAttribute.Name))
            {
                PerformanceCounterCategory.Delete(meterCategoryAttribute.Name);
            }
        }

        [DebuggerStepThrough]
        public static T As<T>(this IMeter obj) where T : class, IMeter
        {
            if(obj is T)
            {
                return (T) obj;
            }
            return default(T);
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

        internal MeterCategory(MeterCategoryAttribute meterCategoryAttribute, IDictionary<T, MeterAttribute> meterAttributes)
        {
            this.meters = new Dictionary<string, IDictionary<T, IMeter>>();
            this.CategoryName = meterCategoryAttribute.Name;
            this.CategoryType = meterCategoryAttribute.MeterCategoryType;
            this.meterAttributes = meterAttributes;

            if(meterCategoryAttribute.MeterCategoryType == MeterCategoryType.SingleInstance)
            {
                var instanceMeters = CreateMeters(meterCategoryAttribute.Name, meterCategoryAttribute.MeterCategoryType, meterAttributes, SingleInstance.DefaultName, InstanceLifetime.Global);
                this.meters.Add(SingleInstance.DefaultName, instanceMeters);
            }
        }

        #endregion

        #region IMeterCategory

        public string CategoryName { get; }

        public MeterCategoryType CategoryType { get; }

        public string[] InstanceNames => this.meters.Keys.ToArray();

        #endregion

        #region IMeterCategory<T>

        public IMeter this[T meterName] => this.GetMeter(meterName);

        public IMeter this[T meterName, string instanceName] => this.GetMeter(meterName, instanceName);

        public void CreateInstance(string instanceName, InstanceLifetime lifetime = InstanceLifetime.Global)
        {
            this.ThrowIfDisposed();
            if (this.CategoryType == MeterCategoryType.SingleInstance)
            {
                throw new InvalidOperationException("Instances cannot be created on meter categories of type \'SingleInstance\'.", null);
            }

            var instanceMeters = CreateMeters(this.CategoryName, this.CategoryType, this.meterAttributes, instanceName, lifetime);
            this.meters.Add(instanceName, instanceMeters);
        }

        public void RemoveInstance(string instanceName)
        {
            this.ThrowIfDisposed();
            if (this.CategoryType == MeterCategoryType.SingleInstance)
            {
                throw new InvalidOperationException("Instances cannot be removed on meter categories of type \'SingleInstance\'.", null);
            }

            var instances = this.meters[instanceName];
            if(instances == null)
            {
                return;
            }

            this.meters.Remove(instanceName);
            foreach(var instance in instances.Values)
            {
                instance.TryDispose();
            }
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

        private static IDictionary<T, IMeter> CreateMeters(string meterCategoryName, MeterCategoryType meterCategoryType, IEnumerable<KeyValuePair<T, MeterAttribute>> meterAttributes, string instanceName, InstanceLifetime instanceLifetime)
        {
            var instanceMeters = new Dictionary<T, IMeter>();

            var enumerator = meterAttributes.GetEnumerator();
            while(enumerator.MoveNext())
            {
                var meterAttribute = enumerator.Current.Value;

                var meter = CreateMeter(meterCategoryName, meterCategoryType, meterAttribute.Name, meterAttribute.MeterType, instanceName, instanceLifetime, meterAttribute.IsReadOnly);

                instanceMeters.Add(enumerator.Current.Key, meter);
            }
            return instanceMeters;
        }

        private static IMeter CreateMeter(string meterCategoryName, MeterCategoryType meterCategoryType, string meterName, MeterType meterType, string instanceName, InstanceLifetime instanceLifetime, bool isReadOnly)
        {
            IMeter meter = null;
            switch(meterType)
            {
                    // Instant Meters
                case MeterType.InstantValue:
                    meter = new InstantValueMeter(meterCategoryName, meterCategoryType, meterName, meterType, instanceName, instanceLifetime, isReadOnly);
                    break;
                case MeterType.InstantPercentage:
                    meter = new InstantPercentageMeter(meterCategoryName, meterCategoryType, meterName, meterType, instanceName, instanceLifetime, isReadOnly);
                    break;
                    // Average Meters
                case MeterType.AverageValue:
                    meter = new AverageValueMeter(meterCategoryName, meterCategoryType, meterName, meterType, instanceName, instanceLifetime, isReadOnly);
                    break;
                case MeterType.AverageTime:
                    meter = new AverageTimeMeter(meterCategoryName, meterCategoryType, meterName, meterType, instanceName, instanceLifetime, isReadOnly);
                    break;
                    // Difference Meters
                case MeterType.DifferentialValue:
                    meter = new DifferentialValueMeter(meterCategoryName, meterCategoryType, meterName, meterType, instanceName, instanceLifetime, isReadOnly);
                    break;
                case MeterType.InstantTime:
                    meter = new InstantTimeMeter(meterCategoryName, meterCategoryType, meterName, meterType, instanceName, instanceLifetime, isReadOnly);
                    break;
                case MeterType.SampleRate:
                    meter = new SampleRateMeter(meterCategoryName, meterCategoryType, meterName, meterType, instanceName, instanceLifetime, isReadOnly);
                    break;
                case MeterType.SamplePercentage:
                    meter = new SamplePercentageMeter(meterCategoryName, meterCategoryType, meterName, meterType, instanceName, instanceLifetime, isReadOnly);
                    break;
                case MeterType.Timer:
                    meter = new TimerMeter(meterCategoryName, meterCategoryType, meterName, meterType, instanceName, instanceLifetime, isReadOnly);
                    break;
                case MeterType.TimerInverse:
                    meter = new TimerInverseMeter(meterCategoryName, meterCategoryType, meterName, meterType, instanceName, instanceLifetime, isReadOnly);
                    break;
                case MeterType.Timer100Ns:
                    meter = new Timer100nsMeter(meterCategoryName, meterCategoryType, meterName, meterType, instanceName, instanceLifetime, isReadOnly);
                    break;
                case MeterType.Timer100NsInverse:
                    meter = new Timer100nsInverseMeter(meterCategoryName, meterCategoryType, meterName, meterType, instanceName, instanceLifetime, isReadOnly);
                    break;
                case MeterType.MultiTimer:
                    meter = new MultiTimerMeter(meterCategoryName, meterCategoryType, meterName, meterType, instanceName, instanceLifetime, isReadOnly);
                    break;
                case MeterType.MultiTimerInverse:
                    meter = new MultiTimerInverseMeter(meterCategoryName, meterCategoryType, meterName, meterType, instanceName, instanceLifetime, isReadOnly);
                    break;
                case MeterType.MultiTimer100Ns:
                    meter = new MultiTimer100NsMeter(meterCategoryName, meterCategoryType, meterName, meterType, instanceName, instanceLifetime, isReadOnly);
                    break;
                case MeterType.MultiTimer100NsInverse:
                    meter = new MultiTimer100NsInverseMeter(meterCategoryName, meterCategoryType, meterName, meterType, instanceName, instanceLifetime, isReadOnly);
                    break;
            }
            return meter;
        }

        private IMeter GetMeter(T meterName, string instanceName = null)
        {
            instanceName = instanceName ?? (this.CategoryType == MeterCategoryType.SingleInstance ? SingleInstance.DefaultName : MultiInstance.DefaultName);
            if(this.meters.ContainsKey(instanceName) && this.meters[instanceName].ContainsKey(meterName))
            {
                return this.meters[instanceName][meterName];
            }
            return null;
        }

        #endregion
    }
}
