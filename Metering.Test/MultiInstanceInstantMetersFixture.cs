﻿using System;
using System.Threading;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NDiagnostics.Metering.Attributes;
using NDiagnostics.Metering.Samples;
using NDiagnostics.Metering.Types;

namespace NDiagnostics.Metering.Test
{
    [TestClass]
    public class MultiInstanceInstantMetersFixture
    {
        #region Initialize/Cleanup

        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            MeterCategory.Install<InstantMultiInstance>();
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            MeterCategory.Uninstall<InstantMultiInstance>();
        }

        #endregion

        #region Test methods


        [TestMethod]
        public void CanCreateInstantCount32MultiInstanceMeter()
        {
            using (var category = MeterCategory.Create<InstantMultiInstance>())
            {
                category.Should().NotBeNull();
                category.CreateInstance(MultiInstance.DefaultName);
                category.CreateInstance("0");
                category.CreateInstance("1");

                var instantCountTotal = category[InstantMultiInstance.InstantCount32].As<IInstantValue>();
                var instantCountZero = category[InstantMultiInstance.InstantCount32, "0"].As<IInstantValue>();
                var instantCountOne = category[InstantMultiInstance.InstantCount32, "1"].As<IInstantValue>();
                instantCountTotal.Should().NotBeNull();
                instantCountTotal.Reset();
                instantCountZero.Should().NotBeNull();
                instantCountZero.Reset();
                instantCountOne.Should().NotBeNull();
                instantCountOne.Reset();

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

        [TestMethod]
        public void CanCreateInstantCount64MultiInstanceMeter()
        {
            using (var category = MeterCategory.Create<InstantMultiInstance>())
            {
                category.Should().NotBeNull();
                category.CreateInstance(MultiInstance.DefaultName);
                category.CreateInstance("0");
                category.CreateInstance("1");

                var instantCountTotal = category[InstantMultiInstance.InstantCount64].As<IInstantValue>();
                var instantCountZero = category[InstantMultiInstance.InstantCount64, "0"].As<IInstantValue>();
                var instantCountOne = category[InstantMultiInstance.InstantCount64, "1"].As<IInstantValue>();
                instantCountTotal.Should().NotBeNull();
                instantCountTotal.Reset();
                instantCountZero.Should().NotBeNull();
                instantCountZero.Reset();
                instantCountOne.Should().NotBeNull();
                instantCountOne.Reset();

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

        [TestMethod]
        public void CanCreateInstantPercentageMultiInstanceMeter()
        {
            using (var category = MeterCategory.Create<InstantMultiInstance>())
            {
                category.Should().NotBeNull();
                category.CreateInstance(MultiInstance.DefaultName);
                category.CreateInstance("0");
                category.CreateInstance("1");

                var instantPercentageTotal = category[InstantMultiInstance.InstantPercentage].As<IInstantPercentage>();
                var instantPercentageZero = category[InstantMultiInstance.InstantPercentage, "0"].As<IInstantPercentage>();
                var instantPercentageOne = category[InstantMultiInstance.InstantPercentage, "1"].As<IInstantPercentage>();
                instantPercentageTotal.Should().NotBeNull();
                instantPercentageTotal.Reset();
                instantPercentageZero.Should().NotBeNull();
                instantPercentageZero.Reset();
                instantPercentageOne.Should().NotBeNull();
                instantPercentageOne.Reset();

                instantPercentageTotal.SetNumerator(1L);
                instantPercentageTotal.SetDenominator(4L);
                instantPercentageTotal.Current.Value().Should().Be(25.0F); // 1/4
                instantPercentageZero.SetNumerator(4L);
                instantPercentageZero.SetDenominator(5L);
                instantPercentageZero.Current.Value().Should().Be(80.0F); // 4/5
                instantPercentageOne.SetNumerator(7L);
                instantPercentageOne.SetDenominator(10L);
                instantPercentageOne.Current.Value().Should().Be(70.0F); // 7/10

                instantPercentageTotal.IncrementNumerator().Should().Be(2L);
                instantPercentageTotal.IncrementDenominator().Should().Be(5L);
                instantPercentageTotal.Current.Value().Should().Be(40.0F); // 2/5
                instantPercentageZero.DecrementNumeratorBy(2L).Should().Be(2L);
                instantPercentageZero.DecrementDenominator().Should().Be(4L);
                instantPercentageZero.Current.Value().Should().Be(50.0F); // 2/4
                instantPercentageOne.IncrementNumerator().Should().Be(8L);
                instantPercentageOne.DecrementDenominatorBy(2L).Should().Be(8L);
                instantPercentageOne.Current.Value().Should().Be(100.0F); // 8/8

                instantPercentageTotal.IncrementNumeratorBy(4L).Should().Be(6L);
                instantPercentageTotal.IncrementDenominatorBy(7L).Should().Be(12L);
                instantPercentageTotal.Current.Value().Should().Be(50.0F); // 6/12
                instantPercentageZero.DecrementNumerator().Should().Be(1L);
                instantPercentageZero.IncrementDenominatorBy(4L).Should().Be(8L);
                instantPercentageZero.Current.Value().Should().Be(12.5F); // 1/8
                instantPercentageOne.DecrementNumeratorBy(2L).Should().Be(6L);
                instantPercentageOne.IncrementDenominatorBy(2L).Should().Be(10L);
                instantPercentageOne.Current.Value().Should().Be(60.0F); // 6/10

                instantPercentageTotal.DecrementNumerator().Should().Be(5L);
                instantPercentageTotal.DecrementDenominatorBy(4L).Should().Be(8L);
                instantPercentageTotal.Current.Value().Should().Be(62.5F); // 5/8
                instantPercentageZero.IncrementNumerator().Should().Be(2L);
                instantPercentageZero.DecrementDenominatorBy(4L).Should().Be(4L);
                instantPercentageZero.Current.Value().Should().Be(50.0F); // 2/4
                instantPercentageOne.DecrementNumerator().Should().Be(5L);
                instantPercentageOne.DecrementDenominator().Should().Be(9L);
                instantPercentageOne.Current.Value().Should().Be(500.0F / 9.0F); // 5/9

                instantPercentageTotal.DecrementNumeratorBy(3L).Should().Be(2L);
                instantPercentageTotal.DecrementDenominator().Should().Be(7L);
                instantPercentageTotal.Current.Value().Should().Be(200.0F / 7.0F); // 2/7
                instantPercentageZero.IncrementNumeratorBy(2L).Should().Be(4L);
                instantPercentageZero.IncrementDenominator().Should().Be(5L);
                instantPercentageZero.Current.Value().Should().Be(80.0F); // 4/5
                instantPercentageOne.IncrementNumeratorBy(2L).Should().Be(7L);
                instantPercentageOne.IncrementDenominator().Should().Be(10L);
                instantPercentageOne.Current.Value().Should().Be(70.0F); // 7/10

                instantPercentageTotal.Reset();
                instantPercentageZero.Reset();
                instantPercentageOne.Reset();
                instantPercentageTotal.Current.Value().Should().Be(0.0F);
                instantPercentageZero.Current.Value().Should().Be(0.0F);
                instantPercentageOne.Current.Value().Should().Be(0.0F);
            }
        }

        [TestMethod]
        public void CanCreateInstantTimeMultiInstanceMeter()
        {
            using (var category = MeterCategory.Create<InstantMultiInstance>())
            {
                category.Should().NotBeNull();
                category.CreateInstance(MultiInstance.DefaultName);
                category.CreateInstance("0");
                category.CreateInstance("1");

                var instantTimeTotal = category[InstantMultiInstance.InstantTime].As<IInstantTime>();
                var instantTimeZero = category[InstantMultiInstance.InstantTime, "0"].As<IInstantTime>();
                var instantTimeOne = category[InstantMultiInstance.InstantTime, "1"].As<IInstantTime>();
                instantTimeTotal.Should().NotBeNull();
                instantTimeTotal.Reset();
                instantTimeZero.Should().NotBeNull();
                instantTimeZero.Reset();
                instantTimeOne.Should().NotBeNull();
                instantTimeOne.Reset();

                instantTimeTotal.Start();
                Thread.Sleep(TimeSpan.FromSeconds(1.0));
                instantTimeZero.Start();
                Thread.Sleep(TimeSpan.FromSeconds(1.0));
                instantTimeOne.Start();

                instantTimeTotal.Current.Value().Should().BeGreaterThan(1.9F);
                instantTimeZero.Current.Value().Should().BeGreaterThan(0.9F);
                instantTimeOne.Current.Value().Should().BeGreaterThan(0.0F);
                instantTimeTotal.Current.Value().Should().BeLessThan(2.1F);
                instantTimeZero.Current.Value().Should().BeLessThan(1.1F);
                instantTimeOne.Current.Value().Should().BeLessThan(0.1F);

                instantTimeTotal.Reset();
                instantTimeZero.Reset();
                instantTimeOne.Reset();

                instantTimeTotal.Current.Value().Should().BeGreaterOrEqualTo(0.0F);
                instantTimeZero.Current.Value().Should().BeGreaterOrEqualTo(0.0F);
                instantTimeOne.Current.Value().Should().BeGreaterOrEqualTo(0.0F);
                instantTimeTotal.Current.Value().Should().BeLessThan(0.1F);
                instantTimeZero.Current.Value().Should().BeLessThan(0.1F);
                instantTimeOne.Current.Value().Should().BeLessThan(0.1F);
            }
        }

        #endregion

        [MeterCategory("Instant Multi Instance", "Instant Multi Instance Description", MeterCategoryType.MultiInstance)]
        public enum InstantMultiInstance
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
