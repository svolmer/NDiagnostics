using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NDiagnostics.Metering.Attributes;

namespace NDiagnostics.Metering.Test
{
    [TestClass]
    public class MeterCategoryFixture
    {
        [TestMethod]
        public void MeterCategoryInstallThrowsIfNotAnEnum()
        {
            Action action = MeterCategory.Install<MeterCategoryNotAnEnum>;
            action.ShouldThrow<NotSupportedException>().WithMessage("Type 'MeterCategoryNotAnEnum' must be an enum.");
        }

        [TestMethod]
        public void MeterCategoryInstallThrowsIfNotDecorated()
        {
            Action action = MeterCategory.Install<MeterCategoryNotDecorated>;
            action.ShouldThrow<NotSupportedException>().WithMessage("Enum 'MeterCategoryNotDecorated' must be decorated by a MeterCategoryAttribute.");
        }

        [TestMethod]
        public void MeterCategoryInstallThrowsIfEmpty()
        {
            Action action = MeterCategory.Install<MeterCategoryEmpty>;
            action.ShouldThrow<NotSupportedException>().WithMessage("Enum 'MeterCategoryEmpty' must contain at least one value.");
        }

        [TestMethod]
        public void MeterCategoryInstallThrowsIfNotAllValuesAreDecorated()
        {
            Action action = MeterCategory.Install<MeterCategoryValueNotDecorated>;
            action.ShouldThrow<NotSupportedException>().WithMessage("Value 'MeterNotDecorated' of enum 'MeterCategoryValueNotDecorated' must de decorated by a MeterAttribute.");
        }

        public class MeterCategoryNotAnEnum
        {            
        }

        public enum MeterCategoryNotDecorated
        {
        }

        [MeterCategory("Meter Category", "Meter Category Description", MeterCategoryType.SingleInstance)]
        public enum MeterCategoryEmpty
        {
        }

        [MeterCategory("Meter Category", "Meter Category Description", MeterCategoryType.SingleInstance)]
        public enum MeterCategoryValueNotDecorated
        {
            [Meter("Meter", "Meter Description", MeterType.InstantValue, MeterDataType.Int64)]
            Meter,

            MeterNotDecorated
        }
    }
}
