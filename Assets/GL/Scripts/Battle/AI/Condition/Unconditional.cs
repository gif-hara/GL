using GL.Battle.CharacterControllers;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Battle.AIControllers
{
    /// <summary>
    /// 無条件でコマンド実行出来る
    /// </summary>
    [CreateAssetMenu(menuName = "GL/AI/Condition/Unconditional")]
    public sealed class Unconditional : Condition
    {
        public override bool Suitable(Character invoker) => true;
    }
}
