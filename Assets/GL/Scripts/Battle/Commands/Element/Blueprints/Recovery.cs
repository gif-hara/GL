using UnityEngine;

namespace GL.Battle.Commands.Element.Blueprints
{
    /// <summary>
    /// 回復コマンドの設定データ.
    /// </summary>
    [CreateAssetMenu(menuName = "GL/Commands/Blueprints/Recovery")]
    public sealed class Recovery : Blueprint
    {
        [SerializeField]
        private Implements.Recovery.Parameter parameter = new Implements.Recovery.Parameter();
        
        public override IImplement Create()
        {
            return new Implements.Recovery(this.parameter);
        }

#if UNITY_EDITOR
        public override string FileName => $"Recovery_{this.parameter.Rate}";

        public override Blueprint SetupFromEditor(string data)
        {
            var s = data.Split('_');
            this.parameter.Rate = float.Parse(s[1]);

            return this;
        }
#endif
    }
}
