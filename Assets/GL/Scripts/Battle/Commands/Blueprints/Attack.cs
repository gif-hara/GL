using UnityEngine;

namespace GL.Scripts.Battle.Commands.Element.Blueprints
{
    /// <summary>
    /// 攻撃コマンドの設定データ.
    /// </summary>
    [CreateAssetMenu(menuName = "GL/Commands/Blueprints/Attack")]
    public sealed class Attack : Blueprint
    {
        [SerializeField]
        private Implements.Attack.Parameter parameter;
        
        public override IImplement Create()
        {
            return new Implements.Attack(this.parameter);
        }
    }
}
