using System;
using System.Linq;

namespace Grax32.EasyInjectCore
{
    internal static class EnumExtensions
    {
        public static bool HasAnyFlag<T>(this T enumValue)
            where T : struct, Enum
        {
            return Enum.GetValues(typeof(T)).OfType<T>().Any(v => enumValue.HasFlag(v));
        }
    }
}
