using GL.Battle.CharacterControllers;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Battle.AIControllers
{
    /// <summary>
    /// 何かしらの影響を受けた場合にコマンド実行出来る
    /// </summary>
    [CreateAssetMenu(menuName = "GL/AI/Condition/ExistAffectedCharacter")]
    public sealed class ExistAffectedCharacter : Condition
    {
        [SerializeField]
        private Constants.TargetPartyType targetPartyType;

        public override bool Suitable(Character invoker)
        {
            var affectedCharacter = invoker.AIController.AffectedCharacter;

            if(affectedCharacter == null)
            {
                return false;
            }

            return invoker.GetTargetPartyType(affectedCharacter) == this.targetPartyType;
        }
    }
}
