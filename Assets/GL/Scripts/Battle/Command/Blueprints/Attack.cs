using UnityEngine;

namespace GL.Scripts.Battle.Command.Blueprints
{
    /// <summary>
    /// 攻撃コマンドの設定データ.
    /// </summary>
    [CreateAssetMenu()]
    public sealed class Attack : Blueprint
    {
        [SerializeField]
        private float rate;
         
        public override ICommand Create()
        {
            return new Command.Attack(this.commandName.Get, this.targetType, this.rate);
        }
    }
}
