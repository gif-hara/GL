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
    }
}
