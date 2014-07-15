using System;
using System.Diagnostics;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NDiagnostics.Metering.Attributes;
using NDiagnostics.Metering.Samples;
using NDiagnostics.Metering.Types;

namespace NDiagnostics.Metering.Test
{
    [TestClass]
    public class AverageMetersFixture
    {
        internal static readonly float TimeTolerance = 1.0F / Stopwatch.Frequency;

        #region Test methods

        [TestMethod]
        public void CanCreateAverageCountSingleInstanceMeter()
        {
            MeterCategory.Install<AverageSingleInstance>();

            try
            {
                using (var category = MeterCategory.Create<AverageSingleInstance>())
                {
                    category.Should().NotBeNull();

                    var averageCount = category[AverageSingleInstance.AverageCount].Cast<IAverageValue>();
                    averageCount.Should().NotBeNull();

                    averageCount.Reset();
                    var sample1 = averageCount.Current;

                    averageCount.Sample(1);
                    averageCount.Sample(2);
                    averageCount.Sample(3);
                    averageCount.Sample(4);
                    averageCount.Sample(5);

                    var sample2 = averageCount.Current;

                    Sample.ComputeValue(sample2, sample1).Should().Be(3.0F);

                    averageCount.Sample(6);
                    averageCount.Sample(7);

                    var sample3 = averageCount.Current;

                    Sample.ComputeValue(sample3, sample1).Should().Be(4.0F);
                }
            }
            finally
            {
                MeterCategory.Uninstall<AverageSingleInstance>();
            }
        }

        [TestMethod]
        public void CanCreateAverageTimeSingleInstanceMeter()
        {
            MeterCategory.Install<AverageSingleInstance>();

            try
            {
                using (var category = MeterCategory.Create<AverageSingleInstance>())
                {
                    category.Should().NotBeNull();

                    var averageTime = category[AverageSingleInstance.AverageTime].Cast<IAverageTime>();
                    averageTime.Should().NotBeNull();

                    averageTime.Reset();
                    var sample1 = averageTime.Current;

                    averageTime.Sample(Time.FromSeconds(1.0F));
                    averageTime.Sample(Time.FromSeconds(1.5F));
                    averageTime.Sample(Time.FromSeconds(2.0F));
                    averageTime.Sample(Time.FromSeconds(2.5F));
                    averageTime.Sample(Time.FromSeconds(3.0F));

                    var sample2 = averageTime.Current;

                    Sample.ComputeValue(sample2, sample1).IsAlmostEqual(2.0F, TimeTolerance).Should().BeTrue();

                    averageTime.Sample(Time.FromSeconds(3.5F));
                    averageTime.Sample(Time.FromSeconds(4.0F));

                    var sample3 = averageTime.Current;

                    Sample.ComputeValue(sample3, sample1).IsAlmostEqual(2.5F, TimeTolerance).Should().BeTrue();
                }
            }
            finally
            {
                MeterCategory.Uninstall<AverageSingleInstance>();
            }
        }


        [TestMethod]
        public void CanCreateAverageRatioSingleInstanceMeter()
        {
            MeterCategory.Install<AverageSingleInstance>();

            try
            {
                using (var category = MeterCategory.Create<AverageSingleInstance>())
                {
                    category.Should().NotBeNull();

                    var averageRatio = category[AverageSingleInstance.AverageRatio].Cast<IAverageRatio>();
                    averageRatio.Should().NotBeNull();

                    averageRatio.Reset();
                    var sample1 = averageRatio.Current;

                    averageRatio.SampleSuccess();
                    averageRatio.SampleSuccess();
                    averageRatio.SampleFailure();
                    averageRatio.SampleSuccess();
                    averageRatio.SampleSuccess();

                    var sample2 = averageRatio.Current;

                    Sample.ComputeValue(sample2, sample1).IsAlmostEqual(400.0F/5.0F).Should().BeTrue();

                    averageRatio.SampleFailure();
                    averageRatio.SampleFailure();
                    averageRatio.SampleSuccess();

                    var sample3 = averageRatio.Current;

                    Sample.ComputeValue(sample3, sample1).IsAlmostEqual(500.0F / 8.0F).Should().BeTrue();
                    Sample.ComputeValue(sample3, sample2).IsAlmostEqual(100.0F / 3.0F).Should().BeTrue();
                }
            }
            finally
            {
                MeterCategory.Uninstall<AverageSingleInstance>();
            }
        }

        #endregion

        [MeterCategory("Average Single Instance", "Average Single Instance Description", MeterCategoryType.SingleInstance)]
        public enum AverageSingleInstance
        {
            [Meter("AverageValue", "AverageValue Description", MeterType.AverageValue)]
            AverageCount,

            [Meter("AverageTime", "AverageTime Description", MeterType.AverageTime)]
            AverageTime,

            [Meter("AverageRatio", "AverageRatio Description", MeterType.AverageRatio)]
            AverageRatio,
        }

        [MeterCategory("Average Multi Instance", "Average Multi Instance Description", MeterCategoryType.MultiInstance)]
        public enum AverageMultiInstance
        {
            [Meter("AverageValue", "AverageValue Description", MeterType.AverageValue)]
            AverageCount,

            [Meter("AverageTime", "AverageTime Description", MeterType.AverageTime)]
            AverageTime,

            [Meter("AverageRatio", "AverageRatio Description", MeterType.AverageRatio)]
            AverageRatio,
        }
    }
}
