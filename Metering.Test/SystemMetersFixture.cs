using System;
using System.Diagnostics;
using System.Threading;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NDiagnostics.Metering.Attributes;
using NDiagnostics.Metering.Samples;

namespace NDiagnostics.Metering.Test
{
    [TestClass]
    public class SystemMetersFixture
    {
        #region Initialize/Cleanup

        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
        }

        [ClassCleanup]
        public static void Cleanup()
        {
        }

        #endregion

        #region Test methods

        [TestMethod]
        public void CanReadValuesFromSystemCategory()
        {
            using(var category = MeterCategory.Create<SystemCategory>())
            {
                category.Should().NotBeNull();

                var systemUpTime = category[SystemCategory.SystemUpTime].As<IInstantTime>();
                var processes = category[SystemCategory.Processes].As<IInstantValue>();
                var contextSwitches = category[SystemCategory.ContextSwitches].As<ISampleRate>();

                systemUpTime.Should().NotBeNull();
                processes.Should().NotBeNull();
                contextSwitches.Should().NotBeNull();

                var currentSystemUpTime = systemUpTime.Current.Value();
                var currentProcesses = processes.Current.Value();

                var sample0 = contextSwitches.Current;
                Thread.Sleep(TimeSpan.FromSeconds(1));
                var sample1 = contextSwitches.Current;
                var currentContextSwitches = Sample.ComputeValue(sample0, sample1);

                Trace.WriteLine("System Up Time : " + currentSystemUpTime);
                Trace.WriteLine("Processes : " + currentProcesses);
                Trace.WriteLine("Context Switches/sec : " + currentContextSwitches);
            }
        }

        [TestMethod]
        public void CanReadValuesFromPhysicalDiskCategory()
        {
            using (var category = MeterCategory.Create<PhysicalDiskCategory>())
            {
                category.Should().NotBeNull();
                category.CreateInstance(MultiInstance.DefaultName);

                var averageTransferTime = category[PhysicalDiskCategory.AverageTransferTime, MultiInstance.DefaultName].As<IAverageTime>();

                averageTransferTime.Should().NotBeNull();

                var sample0 = averageTransferTime.Current;
                Thread.Sleep(TimeSpan.FromSeconds(5));
                var sample1 = averageTransferTime.Current;
                var value = Sample.ComputeValue(sample0, sample1);

                Trace.WriteLine("Avg. Disk sec/Transfer : " + value);
            }
        }


        #endregion

        [MeterCategory("System", "System Description", MeterCategoryType.SingleInstance)]
        public enum SystemCategory
        {
            [Meter("Processes", "Processes Description", MeterType.InstantValue, MeterDataType.Int32, true)]
            Processes,

            [Meter("System Up Time", "System Up Time Description", MeterType.InstantTime, true)]
            SystemUpTime,

            [Meter("Context Switches/sec", "Context Switches/sec Description", MeterType.SampleRate, MeterDataType.Int32, true)]
            ContextSwitches
        }

        [MeterCategory("PhysicalDisk", "PhysicalDisk Description", MeterCategoryType.MultiInstance)]
        public enum PhysicalDiskCategory
        {
            [Meter("Avg. Disk sec/Transfer", "Avg. Disk sec/Transfer Description", MeterType.AverageTime, true)]
            AverageTransferTime
        }
    }
}
