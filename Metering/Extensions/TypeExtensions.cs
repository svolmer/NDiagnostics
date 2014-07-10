using System;
using System.Globalization;
using System.Linq;

namespace NDiagnostics.Metering.Extensions
{
    internal static class TypeExtensions
    {
        #region Public Methods

        public static string ToName(this Type type)
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
