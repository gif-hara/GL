using GL.Battle.CharacterControllers;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Battle.AIControllers
{
    /// <summary>
    /// AIの様々な要素を実行可能かの条件を持つクラス
    /// </summary>
    public abstract class Condition : ScriptableObject
    {
        /// <summary>
        /// 条件を満たしているか返す
        /// </summary>
        public abstract bool Suitable(Character invoker);
    }
}
