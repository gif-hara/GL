using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Battle.AI
{
    /// <summary>
    /// 無条件でコマンド実行出来る
    /// </summary>
    [CreateAssetMenu(menuName = "GL/AI/Condition/Unconditional")]
    public sealed class Unconditional : AICondition
    {
        public override bool Suitable => true;
    }
}
