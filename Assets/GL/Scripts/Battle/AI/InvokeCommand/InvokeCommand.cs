using GL.Battle.CharacterControllers;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Battle.AIControllers
{
    /// <summary>
    /// 実際にコマンドを実行するAI要素
    /// </summary>
    public abstract class InvokeCommand : ScriptableObject
    {
        /// <summary>
        /// コマンドを実行する
        /// </summary>
        public abstract void Invoke(Character character);
    }
}
