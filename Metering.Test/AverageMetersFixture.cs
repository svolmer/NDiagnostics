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
        #region Constants, Properties and Fields

        internal static readonly float TimeTolerance = 1.0F / Stopwatch.Frequency;

        #endregion

        #region Initialize/Cleanup

        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            MeterCategory.Install<AverageSingleInstance>();
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            MeterCategory.Uninstall<AverageSingleInstance>();
        }

        #endregion

        #region Test methods

        [TestMethod]
        public void CanCreateAverageCountSingleInstanceMeter()
        {
            using(var category = MeterCategory.Create<AverageSingleInstance>())
            {
                category.Should().NotBeNull();

                var averageCount = category[AverageSingleInstance.AverageCount].As<IAverageValue>();
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

        [TestMethod]
        public void CanCreateAverageTimeSingleInstanceMeter()
        {
            using(var category = MeterCategory.Create<AverageSingleInstance>())
            {
                category.Should().NotBeNull();

                var averageTime = category[AverageSingleInstance.AverageTime].As<IAverageTime>();
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

        #endregion

        [MeterCategory("Average Single Instance", "Average Single Instance Description", MeterCategoryType.SingleInstance)]
        public enum AverageSingleInstance
        {
            [Meter("AverageValue", "AverageValue Description", MeterType.AverageValue)]
            AverageCount,

            [Meter("AverageTime", "AverageTime Description", MeterType.AverageTime)]
            AverageTime,
        }

        [MeterCategory("Average Multi Instance", "Average Multi Instance Description", MeterCategoryType.MultiInstance)]
        public enum AverageMultiInstance
        {
            [Meter("AverageValue", "AverageValue Description", MeterType.AverageValue)]
            AverageCount,

            [Meter("AverageTime", "AverageTime Description", MeterType.AverageTime)]
            AverageTime,
        }
    }
}
