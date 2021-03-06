﻿using System;
using NDiagnostics.Metering.Extensions;
using NDiagnostics.Metering.Samples;
using NDiagnostics.Metering.Types;

namespace NDiagnostics.Metering.Meters
{
    internal sealed class InstantTimeMeter : Meter<InstantTimeSample>, IInstantTime
    {
        #region Constructors and Destructors

        public InstantTimeMeter(string categoryName, MeterCategoryType categoryType, string meterName, MeterType meterType, string instanceName, InstanceLifetime instanceLifetime, bool isReadOnly)
            : base(categoryName, categoryType, meterName, meterType, instanceName, instanceLifetime, isReadOnly, false)
        {
        }

        #endregion

        #region IInstantTime

        public void Start()
        {
            this.ThrowIfDisposed();
            this.ValueCounter.RawValue = TimeStamp100Ns.Now.Ticks;
        }

        public void Start(DateTime timeStamp)
        {
            this.ThrowIfDisposed();
            this.ValueCounter.RawValue = TimeStamp100Ns.FromDateTime(timeStamp).Ticks;
        }

        #endregion

        #region IMeter

        public override InstantTimeSample Current => this.GetCurrentSample();

        public override void Reset()
        {
            this.ThrowIfDisposed();
            this.ValueCounter.RawValue = TimeStamp100Ns.Now.Ticks;
        }

        #endregion

        #region Methods

        private InstantTimeSample GetCurrentSample()
        {
            var sample = this.ValueCounter.RawSample;
            return new InstantTimeSample(new TimeStamp100Ns(sample.Value), sample.TimeStamp, sample.TimeStamp100Ns);
        }

        #endregion
    }
}
