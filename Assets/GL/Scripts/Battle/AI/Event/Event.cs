using GL.Battle.CharacterControllers;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Battle.AIControllers
{
    /// <summary>
    /// AIの様々なイベントを行う基底クラス
    /// </summary>
    public abstract class Event : ScriptableObject
    {
        /// <summary>
        /// イベントを実行する
        /// </summary>
        public abstract void Invoke(Character invoker);
    }
}
