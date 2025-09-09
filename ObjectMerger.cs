using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DtoTools
{
    public static class ObjectMerger
    {
        public static string MergeClasses<T1, T2>(string mergedClassName)
        {
            var props1 = typeof(T1).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                   .ToDictionary(p => p.Name, p => p);

            var props2 = typeof(T2).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                   .ToDictionary(p => p.Name, p => p);

            string ns1 = typeof(T1).Namespace ?? "(no namespace)";
            string ns2 = typeof(T2).Namespace ?? "(no namespace)";

            var allProps = props1.Keys.Union(props2.Keys).ToList();

            var sb = new StringBuilder();
            sb.AppendLine($"public class {mergedClassName}");
            sb.AppendLine("{");

            foreach (var propName in allProps)
            {
                PropertyInfo? p1 = props1.ContainsKey(propName) ? props1[propName] : null;
                PropertyInfo? p2 = props2.ContainsKey(propName) ? props2[propName] : null;

                // ✅ Always prefer T1's type if it exists
                var chosenProp = p1 ?? p2!;
                string typeName = GetFriendlyTypeName(chosenProp.PropertyType);

                string comments = "";
                if (p1 == null) comments = $" // Missing in {typeof(T1).Name} ({ns1})";
                if (p2 == null) comments = $" // Missing in {typeof(T2).Name} ({ns2})";

                sb.AppendLine($"    public {typeName} {propName} {{ get; set; }}{comments}");
            }

            sb.AppendLine("}");
            return sb.ToString();
        }

        private static string GetFriendlyTypeName(Type type)
        {
            // Value types (int, bool, DateTime, etc.)
            if (type.IsValueType)
            {
                // Handle Nullable<T>
                var underlying = Nullable.GetUnderlyingType(type);
                if (underlying != null)
                {
                    return GetFriendlyTypeName(underlying) + "?";
                }

                return type.Name switch
                {
                    "Int32" => "int",
                    "Boolean" => "bool",
                    "Single" => "float",
                    "Double" => "double",
                    "Byte" => "byte",
                    "DateTime" => "DateTime",
                    _ => type.Name
                };
            }

            // Reference types (string, byte[], etc.)
            if (type == typeof(string)) return "string";
            if (type == typeof(byte[])) return "byte[]";

            return type.Name; // default for other classes
        }
    }
}
