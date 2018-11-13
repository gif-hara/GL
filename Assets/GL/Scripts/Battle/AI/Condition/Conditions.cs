using System;
using GL.Battle.CharacterControllers;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Battle.AIControllers
{
    /// <summary>
    /// <see cref="Condition"/>を複数持つクラス
    /// </summary>
    /// <remarks>
    /// 全ての条件を満たしている場合に<c>true</c>を返す
    /// </remarks>
    [Serializable]
    public sealed class Conditions
    {
        public Condition[] Elements;

        public bool Suitable(Character invoker)
        {
            foreach (var c in this.Elements)
            {
                if (!c.Suitable(invoker))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
