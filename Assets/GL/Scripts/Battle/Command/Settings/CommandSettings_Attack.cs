using UnityEngine;

namespace GL.Scripts.Battle.Command.Settings
{
    /// <summary>
    /// 攻撃コマンドの設定データ.
    /// </summary>
    [CreateAssetMenu()]
    public sealed class CommandSettings_Attack : CommandSettings
    {
        [SerializeField]
        private float rate;
         
        public override ICommand Create()
        {
            return new Command_Attack(this.commandName.Get, this.targetType, this.rate);
        }
    }
}
