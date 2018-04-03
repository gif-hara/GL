using GL.Scripts.Battle.Commands.Impletents;
using UnityEngine;

namespace GL.Scripts.Battle.Commands.Blueprints
{
    /// <summary>
    /// 防御力倍率上昇コマンドの設定データ.
    /// </summary>
    [CreateAssetMenu()]
    public sealed class AddDefenseRate : Blueprint
    {
        public override IImplement Create()
        {
            return new Impletents.AddDefenseRate(this.commandName.Get, this.targetType);
        }
    }
}
