using System;
using NDiagnostics.Metering.Counters;
using NDiagnostics.Metering.Extensions;
using NDiagnostics.Metering.Samples;

namespace NDiagnostics.Metering.Meters
{
    internal abstract class Meter : DisposableObject, IMeter
    {
        #region Constructors and Destructors

        protected Meter(string categoryName, MeterCategoryType categoryType, string meterName, MeterType meterType, string instanceName, MeterInstanceLifetime instanceLifetime, bool createBase)
        {
            categoryName.ThrowIfNullOrEmpty("categoryName");
            meterName.ThrowIfNullOrEmpty("meterName");
            instanceName.ThrowIfNull("instanceName");
            instanceName.ThrowIfExceedsMaxSize("instanceName", 127);

            if (categoryType == MeterCategoryType.SingleInstance && instanceName != SingleInstance.DefaultInstanceName)
            {
                throw new ArgumentException("The meter categories is single-instance and requires the meter to be created without an instance name.");
            }
            if(categoryType == MeterCategoryType.SingleInstance && instanceLifetime != MeterInstanceLifetime.Global)
            {
                throw new ArgumentException("The meter category is single-instance and requires the meter to be created with a global instance lifetime.");
            }
            if(categoryType == MeterCategoryType.MultiInstance && string.IsNullOrEmpty(instanceName))
            {
                throw new InvalidOperationException("The meter category is multi-instance and requires the meter to be created with an unique instance name.");
            }

            this.CategoryName = categoryName;
            this.CategoryType = categoryType;
            this.MeterName = meterName;
            this.MeterType = meterType;
            this.InstanceName = instanceName;
            this.InstanceLifetime = instanceLifetime;

            this.BaseCounter = createBase ? Counters.BaseCounter.Create(categoryName, meterName, instanceName) : null;
            this.ValueCounter = Counters.ValueCounter.Create(categoryName, meterName, instanceName, this.BaseCounter);
        }

        #endregion

        #region Properties

        protected IValueCounter ValueCounter { get; private set; }

        protected IBaseCounter BaseCounter { get; private set; }

        #endregion

        #region IMeter

        public string CategoryName { get; private set; }

        public MeterCategoryType CategoryType { get; private set; }

        public string MeterName { get; private set; }

        public MeterType MeterType { get; private set; }

        public string InstanceName { get; private set; }

        public MeterInstanceLifetime InstanceLifetime { get; private set; }

        public abstract Sample Current { get; }

        public abstract void Reset();

        #endregion

        #region Methods

        protected override void OnDisposing()
        {
            this.BaseCounter.TryDispose();
            this.ValueCounter.TryDispose();
        }

        #endregion
    }
}
