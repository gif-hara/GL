using UnityEngine;

namespace GL.Scripts.Battle.Commands.Element.Blueprints
{
    /// <summary>
    /// 回復コマンドの設定データ.
    /// </summary>
    [CreateAssetMenu(menuName = "GL/Commands/Blueprints/Recovery")]
    public sealed class Recovery : Blueprint
    {
        [SerializeField]
        private Implements.Recovery.Parameter parameter;
        
        public override IImplement Create()
        {
            return new Implements.Recovery(this.parameter);
        }
    }
}
