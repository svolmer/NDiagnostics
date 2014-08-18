using System.Diagnostics;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NDiagnostics.Metering.Attributes;
using NDiagnostics.Metering.Samples;

namespace NDiagnostics.Metering.Test
{
    [TestClass]
    public class SampleMetersFixture
    {
        #region Constants, Properties and Fields

        internal static readonly float TimeTolerance = 1.0F / Stopwatch.Frequency;

        #endregion

        #region Initialize/Cleanup

        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            MeterCategory.Install<SampleSingleInstance>();
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            MeterCategory.Uninstall<SampleSingleInstance>();
        }

        #endregion

        #region Test methods

        [TestMethod]
        public void CanCreateSamplePercentageSingleInstanceMeter()
        {
            using(var category = MeterCategory.Create<SampleSingleInstance>())
            {
                category.Should().NotBeNull();

                var samplePercentage = category[SampleSingleInstance.SamplePercentage].Cast<ISamplePercentage>();
                samplePercentage.Should().NotBeNull();
                samplePercentage.Reset();

                var sample1 = samplePercentage.Current;

                samplePercentage.SampleSuccess();
                samplePercentage.SampleSuccess();
                samplePercentage.SampleFailure();
                samplePercentage.SampleSuccess();
                samplePercentage.SampleSuccess();

                var sample2 = samplePercentage.Current;

                Sample.ComputeValue(sample2, sample1).IsAlmostEqual(400.0F / 5.0F).Should().BeTrue();

                samplePercentage.SampleFailure();
                samplePercentage.SampleFailure();
                samplePercentage.SampleSuccess();

                var sample3 = samplePercentage.Current;

                Sample.ComputeValue(sample3, sample1).IsAlmostEqual(500.0F / 8.0F).Should().BeTrue();
                Sample.ComputeValue(sample3, sample2).IsAlmostEqual(100.0F / 3.0F).Should().BeTrue();
            }
        }

        #endregion

        [MeterCategory("Sample Single Instance", "Sample Single Instance Description", MeterCategoryType.SingleInstance)]
        public enum SampleSingleInstance
        {
            [Meter("SamplePercentage", "SamplePercentage Description", MeterType.SamplePercentage)]
            SamplePercentage,
        }

        [MeterCategory("Average Multi Instance", "Average Multi Instance Description", MeterCategoryType.MultiInstance)]
        public enum AverageMultiInstance
        {
            [Meter("SamplePercentage", "SamplePercentage Description", MeterType.SamplePercentage)]
            SamplePercentage,
        }
    }
}
