using GL.Battle.CharacterControllers;
using GL.User;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Battle.Commands
{
    /// <summary>
    /// コマンドが実行出来る条件を持つクラス
    /// </summary>
    public abstract class CommandElementCondition : ScriptableObject
    {
        /// <summary>
        /// 条件を満たしているか返す
        /// </summary>
        public abstract bool Suitable(Player player);
    }
}
