using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
            using (var category = MeterCategory.Create<SystemCategory>())
            {
                category.Should().NotBeNull();

                var processes = category[SystemCategory.Processes].As<IInstantValue>();
                var systemUpTime = category[SystemCategory.SystemUpTime].As<IInstantTime>();
                var contextSwitches = category[SystemCategory.ContextSwitches].As<ISampleRate>();

                processes.Should().NotBeNull();
                systemUpTime.Should().NotBeNull();
                contextSwitches.Should().NotBeNull();

                var currentProcesses = processes.Current.Value();
                var currentSystemUpTime = systemUpTime.Current.Value();

                var sample0 = contextSwitches.Current;
                Thread.Sleep(TimeSpan.FromSeconds(1));
                var sample1 = contextSwitches.Current;
                var currentContextSwitches = Sample.ComputeValue(sample0, sample1);

                Trace.WriteLine("System Up Time : " + currentSystemUpTime);
                Trace.WriteLine("Processes : " + currentProcesses);
                Trace.WriteLine("Context Switches/sec : " + currentContextSwitches);

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
    }
}
