using System;
using System.Threading;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NDiagnostics.Metering.Attributes;
using NDiagnostics.Metering.Extensions;
using NDiagnostics.Metering.Types;

namespace NDiagnostics.Metering.Test
{
    [TestClass]
    public class InstantMetersFixture
    {
        #region Test methods

        [TestMethod]
        public void CanCreateInstantCount32SingleInstanceMeter()
        {
            MeterCategory.Install<InstantSingleInstance>();

            try
            {
                using(var category = MeterCategory.Create<InstantSingleInstance>())
                {
                    category.Should().NotBeNull();

                    var instantCount = category[InstantSingleInstance.InstantCount32].Cast<IInstantValue>();
                    instantCount.Should().NotBeNull();

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
            finally
            {
                MeterCategory.Uninstall<InstantSingleInstance>();
            }
        }

        [TestMethod]
        public void CanCreateInstantCount64SingleInstanceMeter()
        {
            MeterCategory.Install<InstantSingleInstance>();

            try
            {
                using(var category = MeterCategory.Create<InstantSingleInstance>())
                {
                    category.Should().NotBeNull();

                    var instantCount = category[InstantSingleInstance.InstantCount64].Cast<IInstantValue>();
                    instantCount.Should().NotBeNull();

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
            finally
            {
                MeterCategory.Uninstall<InstantSingleInstance>();
            }
        }

        [TestMethod]
        public void CanCreateInstantRatioSingleInstanceMeter()
        {
            MeterCategory.Install<InstantSingleInstance>();

            try
            {
                using(var category = MeterCategory.Create<InstantSingleInstance>())
                {
                    category.Should().NotBeNull();

                    var instantRatio = category[InstantSingleInstance.InstantRatio].Cast<IInstantRatio>();
                    instantRatio.Should().NotBeNull();

                    instantRatio.SetNumerator(1L);
                    instantRatio.SetDenominator(4L);
                    instantRatio.Current.Value().Should().Be(25.0F); // 1/4

                    instantRatio.IncrementNumerator().Should().Be(2L);
                    instantRatio.IncrementDenominator().Should().Be(5L);
                    instantRatio.Current.Value().Should().Be(40.0F); // 2/5

                    instantRatio.IncrementNumeratorBy(4L).Should().Be(6L);
                    instantRatio.IncrementDenominatorBy(7L).Should().Be(12L);
                    instantRatio.Current.Value().Should().Be(50.0F); // 6/12

                    instantRatio.DecrementNumerator().Should().Be(5L);
                    instantRatio.DecrementDenominatorBy(4L).Should().Be(8L);
                    instantRatio.Current.Value().Should().Be(62.5F); // 5/8

                    instantRatio.DecrementNumeratorBy(3L).Should().Be(2L);
                    instantRatio.DecrementDenominator().Should().Be(7L);
                    instantRatio.Current.Value().Should().Be(200.0F / 7.0F); // 2/7

                    instantRatio.Reset();
                    instantRatio.Current.Value().Should().Be(0.0F);
                }
            }
            finally
            {
                MeterCategory.Uninstall<InstantSingleInstance>();
            }
        }

        [TestMethod]
        public void CanCreateInstantTimeSingleInstanceMeter()
        {
            MeterCategory.Install<InstantSingleInstance>();

            try
            {
                using (var category = MeterCategory.Create<InstantSingleInstance>())
                {
                    category.Should().NotBeNull();

                    var instantTime = category[InstantSingleInstance.InstantTime].Cast<IInstantTime>();
                    instantTime.Should().NotBeNull();

                    instantTime.Set(TimeStamp.Now);
                    instantTime.Current.Value().Should().BeLessThan(0.1F); 

                    Thread.Sleep(new TimeSpan(0,0,0,1)); // 1 second
                    instantTime.Current.Value().Should().BeGreaterThan(1.0F);
                    instantTime.Current.Value().Should().BeLessThan(1.1F);

                    instantTime.Reset(); // = instantTime.Set(TimeStamp.Now);
                    instantTime.Current.Value().Should().BeLessThan(0.1F);
                }
            }
            finally
            {
                MeterCategory.Uninstall<InstantSingleInstance>();
            }
        }

        [TestMethod]
        public void CanCreateInstantCount32MultiInstanceMeter()
        {
            MeterCategory.Install<InstantMultiInstance>();

            try
            {
                string[] instanceNames = {"_Total", "0", "1"};
                using(var category = MeterCategory.Create<InstantMultiInstance>(instanceNames))
                {
                    category.Should().NotBeNull();

                    var instantCountTotal = category[InstantMultiInstance.InstantCount32, "_Total"].Cast<IInstantValue>();
                    var instantCountZero = category[InstantMultiInstance.InstantCount32, "0"].Cast<IInstantValue>();
                    var instantCountOne = category[InstantMultiInstance.InstantCount32, "1"].Cast<IInstantValue>();
                    instantCountTotal.Should().NotBeNull();
                    instantCountZero.Should().NotBeNull();
                    instantCountOne.Should().NotBeNull();

                    instantCountTotal.IncrementBy(6).Should().Be(6);
                    instantCountZero.IncrementBy(2).Should().Be(2);
                    instantCountOne.IncrementBy(4).Should().Be(4);
                    instantCountTotal.Current.Value().Should().Be(6);
                    instantCountZero.Current.Value().Should().Be(2);
                    instantCountOne.Current.Value().Should().Be(4);
                    
                    instantCountTotal.Increment().Should().Be(7);
                    instantCountZero.Increment().Should().Be(3);
                    instantCountTotal.Increment().Should().Be(8);
                    instantCountOne.Increment().Should().Be(5);
                    instantCountTotal.Current.Value().Should().Be(8);
                    instantCountZero.Current.Value().Should().Be(3);
                    instantCountOne.Current.Value().Should().Be(5);

                    instantCountTotal.Decrement().Should().Be(7);
                    instantCountZero.Decrement().Should().Be(2);
                    instantCountTotal.Decrement().Should().Be(6);
                    instantCountOne.Decrement().Should().Be(4);
                    instantCountTotal.Current.Value().Should().Be(6);
                    instantCountZero.Current.Value().Should().Be(2);
                    instantCountOne.Current.Value().Should().Be(4);

                    instantCountTotal.DecrementBy(3).Should().Be(3);
                    instantCountZero.DecrementBy(1).Should().Be(1);
                    instantCountOne.DecrementBy(2).Should().Be(2);
                    instantCountTotal.Current.Value().Should().Be(3);
                    instantCountZero.Current.Value().Should().Be(1);
                    instantCountOne.Current.Value().Should().Be(2);

                    instantCountTotal.Set(6);
                    instantCountZero.Set(4);
                    instantCountOne.Set(2);
                    instantCountTotal.Current.Value().Should().Be(6);
                    instantCountZero.Current.Value().Should().Be(4);
                    instantCountOne.Current.Value().Should().Be(2);

                    instantCountTotal.Reset();
                    instantCountZero.Reset();
                    instantCountOne.Reset();
                    instantCountTotal.Current.Value().Should().Be(0);
                    instantCountZero.Current.Value().Should().Be(0);
                    instantCountOne.Current.Value().Should().Be(0);
                }
            }
            finally
            {
                MeterCategory.Uninstall<InstantMultiInstance>();
            }
        }

        [TestMethod]
        public void CanCreateInstantCount64MultiInstanceMeter()
        {
            MeterCategory.Install<InstantMultiInstance>();

            try
            {
                string[] instanceNames = { "_Total", "0", "1" };
                using (var category = MeterCategory.Create<InstantMultiInstance>(instanceNames))
                {
                    category.Should().NotBeNull();

                    var instantCountTotal = category[InstantMultiInstance.InstantCount64, "_Total"].Cast<IInstantValue>();
                    var instantCountZero = category[InstantMultiInstance.InstantCount64, "0"].Cast<IInstantValue>();
                    var instantCountOne = category[InstantMultiInstance.InstantCount64, "1"].Cast<IInstantValue>();
                    instantCountTotal.Should().NotBeNull();
                    instantCountZero.Should().NotBeNull();
                    instantCountOne.Should().NotBeNull();

                    instantCountTotal.IncrementBy(6).Should().Be(6L);
                    instantCountZero.IncrementBy(2).Should().Be(2L);
                    instantCountOne.IncrementBy(4).Should().Be(4L);
                    instantCountTotal.Current.Value().Should().Be(6L);
                    instantCountZero.Current.Value().Should().Be(2L);
                    instantCountOne.Current.Value().Should().Be(4L);

                    instantCountTotal.Increment().Should().Be(7L);
                    instantCountZero.Increment().Should().Be(3L);
                    instantCountTotal.Increment().Should().Be(8L);
                    instantCountOne.Increment().Should().Be(5L);
                    instantCountTotal.Current.Value().Should().Be(8L);
                    instantCountZero.Current.Value().Should().Be(3L);
                    instantCountOne.Current.Value().Should().Be(5L);

                    instantCountTotal.Decrement().Should().Be(7L);
                    instantCountZero.Decrement().Should().Be(2L);
                    instantCountTotal.Decrement().Should().Be(6L);
                    instantCountOne.Decrement().Should().Be(4L);
                    instantCountTotal.Current.Value().Should().Be(6L);
                    instantCountZero.Current.Value().Should().Be(2L);
                    instantCountOne.Current.Value().Should().Be(4L);

                    instantCountTotal.DecrementBy(3).Should().Be(3L);
                    instantCountZero.DecrementBy(1).Should().Be(1L);
                    instantCountOne.DecrementBy(2).Should().Be(2L);
                    instantCountTotal.Current.Value().Should().Be(3L);
                    instantCountZero.Current.Value().Should().Be(1L);
                    instantCountOne.Current.Value().Should().Be(2L);

                    instantCountTotal.Set(6L);
                    instantCountZero.Set(4L);
                    instantCountOne.Set(2L);
                    instantCountTotal.Current.Value().Should().Be(6L);
                    instantCountZero.Current.Value().Should().Be(4L);
                    instantCountOne.Current.Value().Should().Be(2L);

                    instantCountTotal.Reset();
                    instantCountZero.Reset();
                    instantCountOne.Reset();
                    instantCountTotal.Current.Value().Should().Be(0L);
                    instantCountZero.Current.Value().Should().Be(0L);
                    instantCountOne.Current.Value().Should().Be(0L);
                }
            }
            finally
            {
                MeterCategory.Uninstall<InstantMultiInstance>();
            }
        }

        [TestMethod]
        public void CanCreateInstantRatioMultiInstanceMeter()
        {
            MeterCategory.Install<InstantMultiInstance>();

            try
            {
                string[] instanceNames = { "_Total", "0", "1" };
                using (var category = MeterCategory.Create<InstantMultiInstance>(instanceNames))
                {
                    category.Should().NotBeNull();

                    var instantRatioTotal = category[InstantMultiInstance.InstantRatio, "_Total"].Cast<IInstantRatio>();
                    var instantRatioZero = category[InstantMultiInstance.InstantRatio, "0"].Cast<IInstantRatio>();
                    var instantRatioOne = category[InstantMultiInstance.InstantRatio, "1"].Cast<IInstantRatio>();
                    instantRatioTotal.Should().NotBeNull();
                    instantRatioZero.Should().NotBeNull();
                    instantRatioOne.Should().NotBeNull();

                    instantRatioTotal.SetNumerator(1L);
                    instantRatioTotal.SetDenominator(4L);
                    instantRatioTotal.Current.Value().Should().Be(25.0F); // 1/4
                    instantRatioZero.SetNumerator(4L);
                    instantRatioZero.SetDenominator(5L);
                    instantRatioZero.Current.Value().Should().Be(80.0F); // 4/5
                    instantRatioOne.SetNumerator(7L);
                    instantRatioOne.SetDenominator(10L);
                    instantRatioOne.Current.Value().Should().Be(70.0F); // 7/10

                    instantRatioTotal.IncrementNumerator().Should().Be(2L);
                    instantRatioTotal.IncrementDenominator().Should().Be(5L);
                    instantRatioTotal.Current.Value().Should().Be(40.0F); // 2/5
                    instantRatioZero.DecrementNumeratorBy(2L).Should().Be(2L);
                    instantRatioZero.DecrementDenominator().Should().Be(4L);
                    instantRatioZero.Current.Value().Should().Be(50.0F); // 2/4
                    instantRatioOne.IncrementNumerator().Should().Be(8L);
                    instantRatioOne.DecrementDenominatorBy(2L).Should().Be(8L);
                    instantRatioOne.Current.Value().Should().Be(100.0F); // 8/8

                    instantRatioTotal.IncrementNumeratorBy(4L).Should().Be(6L);
                    instantRatioTotal.IncrementDenominatorBy(7L).Should().Be(12L);
                    instantRatioTotal.Current.Value().Should().Be(50.0F); // 6/12
                    instantRatioZero.DecrementNumerator().Should().Be(1L);
                    instantRatioZero.IncrementDenominatorBy(4L).Should().Be(8L);
                    instantRatioZero.Current.Value().Should().Be(12.5F); // 1/8
                    instantRatioOne.DecrementNumeratorBy(2L).Should().Be(6L);
                    instantRatioOne.IncrementDenominatorBy(2L).Should().Be(10L);
                    instantRatioOne.Current.Value().Should().Be(60.0F); // 6/10

                    instantRatioTotal.DecrementNumerator().Should().Be(5L);
                    instantRatioTotal.DecrementDenominatorBy(4L).Should().Be(8L);
                    instantRatioTotal.Current.Value().Should().Be(62.5F); // 5/8
                    instantRatioZero.IncrementNumerator().Should().Be(2L);
                    instantRatioZero.DecrementDenominatorBy(4L).Should().Be(4L);
                    instantRatioZero.Current.Value().Should().Be(50.0F); // 2/4
                    instantRatioOne.DecrementNumerator().Should().Be(5L);
                    instantRatioOne.DecrementDenominator().Should().Be(9L);
                    instantRatioOne.Current.Value().Should().Be(500.0F / 9.0F); // 5/9

                    instantRatioTotal.DecrementNumeratorBy(3L).Should().Be(2L);
                    instantRatioTotal.DecrementDenominator().Should().Be(7L);
                    instantRatioTotal.Current.Value().Should().Be(200.0F / 7.0F); // 2/7
                    instantRatioZero.IncrementNumeratorBy(2L).Should().Be(4L);
                    instantRatioZero.IncrementDenominator().Should().Be(5L);
                    instantRatioZero.Current.Value().Should().Be(80.0F); // 4/5
                    instantRatioOne.IncrementNumeratorBy(2L).Should().Be(7L);
                    instantRatioOne.IncrementDenominator().Should().Be(10L);
                    instantRatioOne.Current.Value().Should().Be(70.0F); // 7/10

                    instantRatioTotal.Reset();
                    instantRatioZero.Reset();
                    instantRatioOne.Reset();
                    instantRatioTotal.Current.Value().Should().Be(0.0F);
                    instantRatioZero.Current.Value().Should().Be(0.0F);
                    instantRatioOne.Current.Value().Should().Be(0.0F);
                }
            }
            finally
            {
                MeterCategory.Uninstall<InstantSingleInstance>();
            }
        }

        [TestMethod]
        public void CanCreateInstantTimeMultiInstanceMeter()
        {
            MeterCategory.Install<InstantMultiInstance>();

            try
            {
                string[] instanceNames = { "_Total", "0", "1" };
                using (var category = MeterCategory.Create<InstantMultiInstance>(instanceNames))
                {
                    category.Should().NotBeNull();

                    var instantTimeTotal = category[InstantMultiInstance.InstantTime, instanceNames[0]].Cast<IInstantTime>();
                    var instantTimeZero = category[InstantMultiInstance.InstantTime, instanceNames[1]].Cast<IInstantTime>();
                    var instantTimeOne = category[InstantMultiInstance.InstantTime, instanceNames[2]].Cast<IInstantTime>();
                    instantTimeTotal.Should().NotBeNull();
                    instantTimeZero.Should().NotBeNull();
                    instantTimeOne.Should().NotBeNull();

                    instantTimeTotal.Set(TimeStamp.Now);
                    Thread.Sleep(TimeSpan.FromSeconds(1.0));
                    instantTimeZero.Set(TimeStamp.Now);
                    Thread.Sleep(TimeSpan.FromSeconds(1.0));
                    instantTimeOne.Set(TimeStamp.Now);

                    instantTimeTotal.Current.Value().Should().BeGreaterThan(2.0F);
                    instantTimeZero.Current.Value().Should().BeGreaterThan(1.0F);
                    instantTimeOne.Current.Value().Should().BeGreaterThan(0.0F);
                    instantTimeTotal.Current.Value().Should().BeLessThan(2.1F);
                    instantTimeZero.Current.Value().Should().BeLessThan(1.1F);
                    instantTimeOne.Current.Value().Should().BeLessThan(0.1F);

                    instantTimeTotal.Reset();
                    instantTimeZero.Reset();
                    instantTimeOne.Reset();

                    instantTimeTotal.Current.Value().Should().BeGreaterThan(0.0F);
                    instantTimeZero.Current.Value().Should().BeGreaterThan(0.0F);
                    instantTimeOne.Current.Value().Should().BeGreaterThan(0.0F);
                    instantTimeTotal.Current.Value().Should().BeLessThan(0.1F);
                    instantTimeZero.Current.Value().Should().BeLessThan(0.1F);
                    instantTimeOne.Current.Value().Should().BeLessThan(0.1F);
                }
            }
            finally
            {
                MeterCategory.Uninstall<InstantSingleInstance>();
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

            [Meter("InstantRatio", "InstantRatio Description", MeterType.InstantRatio)]
            InstantRatio,

            [Meter("InstantTime", "InstantTime Description", MeterType.InstantTime)]
            InstantTime,
        }

        [MeterCategory("Instant Multi Instance", "Instant Multi Instance Description", MeterCategoryType.MultiInstance)]
        public enum InstantMultiInstance
        {
            [Meter("InstantCount32", "InstantCount32 Description", MeterType.InstantValue, MeterDataType.Int32)]
            InstantCount32,

            [Meter("InstantCount64", "InstantCount64 Description", MeterType.InstantValue, MeterDataType.Int64)]
            InstantCount64,

            [Meter("InstantRatio", "InstantRatio Description", MeterType.InstantRatio)]
            InstantRatio,

            [Meter("InstantTime", "InstantTime Description", MeterType.InstantTime)]
            InstantTime,
        }
    }
}
