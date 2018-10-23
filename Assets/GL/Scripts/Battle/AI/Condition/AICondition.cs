using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Battle.AI
{
    /// <summary>
    /// <see cref="AIElement"/>を実行する条件を持つクラス
    /// </summary>
    public abstract class AICondition : ScriptableObject
    {
        /// <summary>
        /// 条件を満たしているか返す
        /// </summary>
        public abstract bool Suitable { get; }
    }
}
