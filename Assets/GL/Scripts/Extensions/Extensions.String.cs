using UnityEngine;

namespace GL.Extensions
{
    /// <summary>
    /// <see cref="string"/>の拡張クラス
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// 改行コードを削除した文字列を返す
        /// </summary>
        public static string RemoveNewLine(this string self)
        {
            return self.Replace("¥r", "").Replace("¥n", "").Replace("\r", "").Replace("\n", "");
        }

        /// <summary>
        /// 最後にある改行コードを削除する
        /// </summary>
        public static string RemoveLastNewLine(this string self)
        {
            while(self.LastIndexOf(System.Environment.NewLine) == self.Length - 1)
            {
                self = self.Remove(self.Length - 1);
            }

            return self;
        }
    }
}
