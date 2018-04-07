using GL.Scripts.Battle.Commands.Impletents;
using UnityEngine;

namespace GL.Scripts.Battle.Commands.Blueprints
{
    /// <summary>
    /// 攻撃力倍率上昇コマンドの設定データ.
    /// </summary>
    [CreateAssetMenu(menuName = "GL/Commands/Blueprints/StrengthUpRate")]
    public sealed class StrengthUpRate : Blueprint
    {
        public override IImplement Create()
        {
            return new Impletents.StrengthUpRate(this.commandName.Get, this.targetPartyType, this.targetType);
        }
    }
}
