using System;
using NDiagnostics.Metering.Extensions;

namespace NDiagnostics.Metering.Attributes
{
    [AttributeUsage(AttributeTargets.Enum, AllowMultiple = false, Inherited = false)]
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

        public string Name { get; private set; }

        public string Description { get; private set; }

        public MeterCategoryType MeterCategoryType { get; private set; }

        #endregion
    }
}
