using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DtoTools
{
    public static class ClassComparer
    {
        public static void CompareClasses<T1, T2>()
        {
            var type1Props = typeof(T1).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                       .Select(p => p.Name)
                                       .ToHashSet();

            var type2Props = typeof(T2).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                       .Select(p => p.Name)
                                       .ToHashSet();

            var missingInT2 = type1Props.Except(type2Props).ToList();
            var missingInT1 = type2Props.Except(type1Props).ToList();

            Console.WriteLine($"🔹 Missing in {typeof(T2).Name}: {string.Join(", ", missingInT2)}");
            Console.WriteLine($"🔹 Missing in {typeof(T1).Name}: {string.Join(", ", missingInT1)}");
        }
    }
}
