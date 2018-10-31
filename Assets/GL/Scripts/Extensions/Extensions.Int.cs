namespace GL.Extensions
{
    /// <summary>
    /// <see cref="int"/>の拡張クラス
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// 整数を百分率にして返す
        /// </summary>
        /// <remarks>
        /// <c>100</c> = <c>1.0f</c>
        /// </remarks>
        public static float ToPercentage(this int self)
        {
            return self / 100.0f;
        }
    }
}
