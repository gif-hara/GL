using GL.Scripts.Battle.Commands.Impletents;
using UnityEngine;

namespace GL.Scripts.Battle.Commands.Blueprints
{
    /// <summary>
    /// 攻撃コマンドの設定データ.
    /// </summary>
    [CreateAssetMenu(menuName = "GL/Commands/Blueprints/Attack")]
    public sealed class Attack : Blueprint
    {
        [SerializeField]
        private Impletents.Attack.AttackParameter parameter;
         
        public override IImplement Create()
        {
            return new Impletents.Attack(this.parameter);
        }
    }
}
