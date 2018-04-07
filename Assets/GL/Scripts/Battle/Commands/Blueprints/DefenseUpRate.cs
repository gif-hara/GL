using GL.Scripts.Battle.Commands.Impletents;
using UnityEngine;

namespace GL.Scripts.Battle.Commands.Blueprints
{
    /// <summary>
    /// 防御力倍率上昇コマンドの設定データ.
    /// </summary>
    [CreateAssetMenu(menuName = "GL/Commands/Blueprints/DefenseUpRate")]
    public sealed class DefenseUpRate : Blueprint
    {
        public override IImplement Create()
        {
            return new Impletents.DefenseUpRate(this.commandName.Get, this.targetPartyType, this.targetType);
        }
    }
}
