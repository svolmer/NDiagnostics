using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace NDiagnostics.Metering.Extensions
{
    [DebuggerStepThrough]
    internal static class TypeExtensions
    {
        #region Methods

        internal static T ThrowIfNotEnum<T>(this T type)
            where T : Type
        {
            if(!type.IsEnum)
            {
                throw new NotSupportedException(string.Format("Type '{0}' must be an enum.", type.ToName()));
            }
            return type;
        }

        internal static string ToName(this Type type)
        {
            var typeName = type.Name;
            if(type.IsGenericType)
            {
                var typeArguments = type.GetGenericArguments();
                var c = typeArguments.Count().ToString(CultureInfo.InvariantCulture).Length + 1;
                typeName = typeName.Remove(typeName.Length - c, c);
                typeName += "<";
                typeName = typeArguments.Aggregate(typeName, (current, typeArgument) => current + (typeArgument.Name + ","));
                typeName = typeName.Remove(typeName.Length - 1, 1);
                typeName += ">";
            }
            return typeName;
        }

        #endregion
    }
}
