using System;
using System.Threading;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NDiagnostics.Metering.Attributes;
using NDiagnostics.Metering.Samples;
using NDiagnostics.Metering.Types;

namespace NDiagnostics.Metering.Test
{
    [TestClass]
    public class SingleInstanceInstantMetersFixture
    {
        #region Initialize/Cleanup

        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            MeterCategory.Install<InstantSingleInstance>();
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            MeterCategory.Uninstall<InstantSingleInstance>();
        }

        #endregion

        #region Test methods

        [TestMethod]
        public void CanCreateInstantCount32SingleInstanceMeter()
        {
            using(var category = MeterCategory.Create<InstantSingleInstance>())
            {
                category.Should().NotBeNull();
                category.InstanceNames.ShouldBeEquivalentTo(new[] {SingleInstance.DefaultInstanceName});

                var instantCount = category[InstantSingleInstance.InstantCount32].As<IInstantValue>();
                instantCount.Should().NotBeNull();
                instantCount.Reset();

                instantCount.Increment().Should().Be(1);
                instantCount.Current.Value().Should().Be(1);

                instantCount.IncrementBy(4).Should().Be(5);
                instantCount.Current.Value().Should().Be(5);

                instantCount.Decrement().Should().Be(4);
                instantCount.Current.Value().Should().Be(4);

                instantCount.DecrementBy(2).Should().Be(2);
                instantCount.Current.Value().Should().Be(2);

                instantCount.Set(6);
                instantCount.Current.Value().Should().Be(6);

                instantCount.Reset();
                instantCount.Current.Value().Should().Be(0);
            }
        }

        [TestMethod]
        public void CanCreateInstantCount64SingleInstanceMeter()
        {
            using(var category = MeterCategory.Create<InstantSingleInstance>())
            {
                category.Should().NotBeNull();

                var instantCount = category[InstantSingleInstance.InstantCount64].As<IInstantValue>();
                instantCount.Should().NotBeNull();
                instantCount.Reset();

                instantCount.Increment().Should().Be(1L);
                instantCount.Current.Value().Should().Be(1L);

                instantCount.IncrementBy(4).Should().Be(5L);
                instantCount.Current.Value().Should().Be(5L);

                instantCount.Decrement().Should().Be(4L);
                instantCount.Current.Value().Should().Be(4L);

                instantCount.DecrementBy(2).Should().Be(2L);
                instantCount.Current.Value().Should().Be(2L);

                instantCount.Set(6L);
                instantCount.Current.Value().Should().Be(6L);

                instantCount.Reset();
                instantCount.Current.Value().Should().Be(0L);
            }
        }

        [TestMethod]
        public void CanCreateInstantPercentageSingleInstanceMeter()
        {
            using(var category = MeterCategory.Create<InstantSingleInstance>())
            {
                category.Should().NotBeNull();

                var instantPercentage = category[InstantSingleInstance.InstantPercentage].As<IInstantPercentage>();
                instantPercentage.Should().NotBeNull();
                instantPercentage.Reset();

                instantPercentage.SetNumerator(1L);
                instantPercentage.SetDenominator(4L);
                instantPercentage.Current.Value().Should().Be(25.0F); // 1/4

                instantPercentage.IncrementNumerator().Should().Be(2L);
                instantPercentage.IncrementDenominator().Should().Be(5L);
                instantPercentage.Current.Value().Should().Be(40.0F); // 2/5

                instantPercentage.IncrementNumeratorBy(4L).Should().Be(6L);
                instantPercentage.IncrementDenominatorBy(7L).Should().Be(12L);
                instantPercentage.Current.Value().Should().Be(50.0F); // 6/12

                instantPercentage.DecrementNumerator().Should().Be(5L);
                instantPercentage.DecrementDenominatorBy(4L).Should().Be(8L);
                instantPercentage.Current.Value().Should().Be(62.5F); // 5/8

                instantPercentage.DecrementNumeratorBy(3L).Should().Be(2L);
                instantPercentage.DecrementDenominator().Should().Be(7L);
                instantPercentage.Current.Value().Should().Be(200.0F / 7.0F); // 2/7

                instantPercentage.Reset();
                instantPercentage.Current.Value().Should().Be(0.0F);
            }
        }

        [TestMethod]
        public void CanCreateInstantTimeSingleInstanceMeter()
        {
            using(var category = MeterCategory.Create<InstantSingleInstance>())
            {
                category.Should().NotBeNull();

                var instantTime = category[InstantSingleInstance.InstantTime].As<IInstantTime>();
                instantTime.Should().NotBeNull();
                instantTime.Reset();

                instantTime.Set(TimeStamp.Now);
                instantTime.Current.Value().Should().BeLessThan(0.1F);

                Thread.Sleep(new TimeSpan(0, 0, 0, 1)); // 1 second
                instantTime.Current.Value().Should().BeGreaterThan(1.0F);
                instantTime.Current.Value().Should().BeLessThan(1.1F);

                instantTime.Reset(); // = instantTime.Set(TimeStamp.Now);
                instantTime.Current.Value().Should().BeLessThan(0.1F);
            }
        }

        #endregion

        [MeterCategory("Instant Single Instance", "Instant Single Instance Description", MeterCategoryType.SingleInstance)]
        public enum InstantSingleInstance
        {
            [Meter("InstantCount32", "InstantCount32 Description", MeterType.InstantValue, MeterDataType.Int32)]
            InstantCount32,

            [Meter("InstantCount64", "InstantCount64 Description", MeterType.InstantValue, MeterDataType.Int64)]
            InstantCount64,

            [Meter("InstantPercentage", "InstantPercentage Description", MeterType.InstantPercentage)]
            InstantPercentage,

            [Meter("InstantTime", "InstantTime Description", MeterType.InstantTime)]
            InstantTime,
        }
    }
}
