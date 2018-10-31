using UnityEngine;

namespace GL.Extensions
{
    /// <summary>
    /// <see cref="int"/>の拡張クラス
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// 浮動小数点を100倍にして返す
        /// </summary>
        /// <remarks>
        /// <c>1.0f</c> = <c>100</c>
        /// </remarks>
        public static int ToPercentage(this float self)
        {
            return Mathf.FloorToInt(self * 100.0f);
        }
    }
}
