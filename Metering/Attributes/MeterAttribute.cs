using System;
using NDiagnostics.Metering.Extensions;

namespace NDiagnostics.Metering.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public sealed class MeterAttribute : Attribute
    {
        #region Constructors and Destructors

        public MeterAttribute(string name, string description, MeterType meterType)
        {
            name.ThrowIfNullOrWhiteSpace("name");
            description.ThrowIfNullOrWhiteSpace("description");

            this.Name = name;
            this.Description = description;
            this.MeterType = meterType;
            this.DataType = MeterDataType.Int64;
        }

        public MeterAttribute(string name, string description, MeterType meterType, MeterDataType dataType)
            : this(name, description, meterType)
        {
            this.DataType = dataType;
        }

        #endregion

        #region Properties

        public string Name { get; private set; }

        public string Description { get; private set; }

        public MeterType MeterType { get; private set; }

        public MeterDataType DataType { get; private set; }

        #endregion
    }
}
