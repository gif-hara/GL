using UnityEngine;

namespace GL.Battle.Commands.Element.Blueprints
{
    /// <summary>
    /// 固定値回復コマンドの設定データ.
    /// </summary>
    [CreateAssetMenu(menuName = "GL/Commands/Blueprints/RecoveryFixed")]
    public sealed class RecoveryFixed : Blueprint
    {
        [SerializeField]
        private Implements.RecoveryFixed.Parameter parameter;
        
        public override IImplement Create()
        {
            return new Implements.RecoveryFixed(this.parameter);
        }

#if UNITY_EDITOR
        public override string FileName => $"{this.parameter.Value}";
#endif
    }
}
