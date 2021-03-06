﻿using System;
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
                throw new NotSupportedException($"Type '{type.ToName()}' must be an enum.");
            }
            return type;
        }

        internal static string ToName<T>(this T type)
            where T : Type
        {
            var typeName = type.Name;
            if(type.IsGenericType)
            {
                var typeArguments = type.GetGenericArguments();
                var c = typeArguments.Length.ToString(CultureInfo.InvariantCulture).Length + 1;
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
