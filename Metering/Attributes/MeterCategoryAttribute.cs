using System;
using NDiagnostics.Metering.Extensions;

namespace NDiagnostics.Metering.Attributes
{
    [AttributeUsage(AttributeTargets.Enum)]
    public sealed class MeterCategoryAttribute : Attribute
    {
        #region Constructors and Destructors

        public MeterCategoryAttribute(string name, string description, MeterCategoryType type)
        {
            name.ThrowIfNullOrWhiteSpace("name");
            description.ThrowIfNullOrWhiteSpace("description");

            this.Name = name;
            this.Description = description;
            this.MeterCategoryType = type;
        }

        #endregion

        #region Properties

        public string Name { get; }

        public string Description { get; }

        public MeterCategoryType MeterCategoryType { get; }

        #endregion
    }
}
