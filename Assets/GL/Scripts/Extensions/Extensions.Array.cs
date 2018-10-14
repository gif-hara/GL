using System.Collections.Generic;
using System;
using System.Linq;

namespace HK.GL.Extensions
{
    /// <summary>
    /// <see cref="Array{T}"/>の拡張クラス
    /// </summary>
    public static partial class Extensions
    {
        public static void ForEach<T>(this T[] self, Action<T> action)
        {
            Array.ForEach(self, action);
        }

        public static T Find<T>(this T[] self, Predicate<T> match)
        {
            return Array.Find(self, match);
        }

        public static T[] FindAll<T>(this T[] self, Predicate<T> match)
        {
            return Array.FindAll(self, match);
        }
    }
}
