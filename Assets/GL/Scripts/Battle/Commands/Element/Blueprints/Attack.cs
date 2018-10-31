using UnityEngine;

namespace GL.Battle.Commands.Element.Blueprints
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

#if UNITY_EDITOR
        public override string FileName => $"Attack_{this.parameter.Rate}_{this.parameter.AttributeType}_{this.parameter.LogicType}";
#endif
    }
}
