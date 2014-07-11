using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NDiagnostics.Metering.Attributes;

namespace NDiagnostics.Metering.Test
{
    [TestClass]
    public class AverageMetersFixture
    {
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
            [Meter("AverageValue1", "AverageValue1 Description", MeterType.AverageValue)]
            AverageCount,

            [Meter("AverageTime", "AverageTime Description", MeterType.AverageTime)]
            AverageTime,
        }
    }
}
