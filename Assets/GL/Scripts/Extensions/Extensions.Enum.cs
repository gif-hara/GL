using System;
using GL.Scripts.Battle.Systems;

namespace GL.Scripts.Extensions
{
    /// <summary>
    /// <see cref="Enum"/>の拡張クラス
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// 有利な状態異常であるか返す
        /// </summary>
        public static bool IsPositive(this Constants.StatusAilmentType self)
        {
            return (int)self < 100;
        }

        /// <summary>
        /// 不利な状態異常であるか返す
        /// </summary>
        public static bool IsNegative(this Constants.StatusAilmentType self)
        {
            return !self.IsPositive();
        }
    }
}
