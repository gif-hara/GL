using GL.Battle.CharacterControllers;
using GL.User;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Battle.Commands
{
    /// <summary>
    /// 無条件でコマンド実行出来る
    /// </summary>
    [CreateAssetMenu(menuName = "GL/Commands/Condition/Unconditional")]
    public sealed class Unconditional : CommandElementCondition
    {
        public override bool Suitable(Player player)
        {
            return true;
        }
    }
}
