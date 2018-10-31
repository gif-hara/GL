using System;
using GL.Battle;

namespace GL.Extensions
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

        /// <summary>
        /// ターゲット選択可能なタイプか返す
        /// </summary>
        public static bool IsSelectType(this Constants.TargetType self)
        {
            return self == Constants.TargetType.Select || self == Constants.TargetType.SelectRange;
        }
    }
}
