using System;
using NDiagnostics.Metering.Extensions;

namespace NDiagnostics.Metering.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public sealed class MeterAttribute : Attribute
    {
        #region Constructors and Destructors

        public MeterAttribute(string name, string description, MeterType meterType)
            : this(name, description, meterType, MeterDataType.Int64, false)
        {
        }

        public MeterAttribute(string name, string description, MeterType meterType, bool isReadOnly)
            : this(name, description, meterType, MeterDataType.Int64, isReadOnly)
        {
        }

        public MeterAttribute(string name, string description, MeterType meterType, MeterDataType dataType)
            : this(name, description, meterType, dataType, false)
        {
        }

        public MeterAttribute(string name, string description, MeterType meterType, MeterDataType dataType, bool isReadOnly)
        {
            name.ThrowIfNullOrWhiteSpace("name");
            description.ThrowIfNullOrWhiteSpace("description");

            this.Name = name;
            this.Description = description;
            this.MeterType = meterType;
            this.DataType = dataType;
            this.IsReadOnly = isReadOnly;
        }

        #endregion

        #region Properties

        public string Name { get; private set; }

        public string Description { get; private set; }

        public MeterType MeterType { get; private set; }

        public MeterDataType DataType { get; private set; }

        public bool IsReadOnly { get; private set; }

        #endregion
    }
}
