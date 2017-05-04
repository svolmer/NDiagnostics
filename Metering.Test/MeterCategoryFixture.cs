using System;
using System.Diagnostics;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NDiagnostics.Metering.Attributes;

namespace NDiagnostics.Metering.Test
{
    [TestClass]
    public class MeterCategoryFixture
    {
        [TestMethod]
        public void MeterCategoryInstallThrowsIfNotDecorated()
        {
            Action action = MeterCategory.Install<CategoryNotDecorated>;
            action.ShouldThrow<NotSupportedException>().WithMessage("Enum 'CategoryNotDecorated' must be decorated by a MeterCategory attribute.");
        }

        [TestMethod]
        public void MeterCategoryInstallThrowsIfEmpty()
        {
            Action action = MeterCategory.Install<CategoryEmpty>;
            action.ShouldThrow<NotSupportedException>().WithMessage("Enum 'CategoryEmpty' must contain at least one value.");
        }

        [TestMethod]
        public void MeterCategoryInstallThrowsIfNotAllValuesAreDecorated()
        {
            Action action = MeterCategory.Install<CategoryValueNotDecorated>;
            action.ShouldThrow<NotSupportedException>().WithMessage("Value 'NotDecorated' of enum 'CategoryValueNotDecorated' must de decorated by a Meter attribute.");
        }

        [TestMethod]
        public void MeterCategoryInstallInstallsMeters()
        {
            try
            {
                MeterCategory.Install<Category>();

                PerformanceCounterCategory.Exists("Meter Category").Should().BeTrue();
                PerformanceCounterCategory.CounterExists("Meter", "Meter Category").Should().BeTrue();
            }
            finally
            {
                MeterCategory.Uninstall<Category>();
            }
        }

        public enum CategoryNotDecorated
        {
        }

        [MeterCategory("Meter Category", "Meter Category Description", MeterCategoryType.SingleInstance)]
        public enum CategoryEmpty
        {
        }

        [MeterCategory("Meter Category", "Meter Category Description", MeterCategoryType.SingleInstance)]
        public enum CategoryValueNotDecorated
        {
            [Meter("Meter", "Meter Description", MeterType.InstantValue, MeterDataType.Int64)]
            Meter,

            NotDecorated
        }

        [MeterCategory("Meter Category", "Meter Category Description", MeterCategoryType.SingleInstance)]
        public enum Category
        {
            [Meter("Meter", "Meter Description", MeterType.InstantValue, MeterDataType.Int64)]
            Meter,
        }
    }
}
