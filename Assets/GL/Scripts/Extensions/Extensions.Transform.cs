using UnityEngine;

namespace HK.GL.Extensions
{
    /// <summary>
    /// <see cref="Transform"/>の拡張クラス
    /// </summary>
    public static partial class Extensions
    {
        public static void ReversalChildren(this Transform self)
        {
            var count = self.childCount;
            var children = new Transform[count];
            for (var i = 0; i < count; ++i)
            {
                children[i] = self.GetChild(i);
            }

            for (var i = 0; i < count; ++i)
            {
                children[count - 1 - i].SetAsLastSibling();
            }
        }
    }
}
