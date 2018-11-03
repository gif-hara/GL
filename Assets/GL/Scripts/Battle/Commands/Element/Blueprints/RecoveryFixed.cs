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
        public override string FileName => $"RecoveryFixed_{this.parameter.Value}";

        public override Blueprint SetupFromEditor(string data)
        {
            var s = data.Split('_');
            this.parameter.Value = int.Parse(s[1]);

            return this;
        }
#endif
    }
}
