using UnityEngine;

namespace GL.Scripts.Battle.Command.Settings
{
    /// <summary>
    /// 防御力倍率上昇コマンドの設定データ.
    /// </summary>
    [CreateAssetMenu()]
    public sealed class CommandSettings_AddDefenseRate : CommandSettings
    {
        public override ICommand Create()
        {
            return new Command_AddDefenseRate(this.commandName.Get, this.targetType);
        }
    }
}
