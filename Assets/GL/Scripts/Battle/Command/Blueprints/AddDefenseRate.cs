using UnityEngine;

namespace GL.Scripts.Battle.Command.Blueprints
{
    /// <summary>
    /// 防御力倍率上昇コマンドの設定データ.
    /// </summary>
    [CreateAssetMenu()]
    public sealed class AddDefenseRate : Blueprint
    {
        public override ICommand Create()
        {
            return new Command.AddDefenseRate(this.commandName.Get, this.targetType);
        }
    }
}
