using GL.Scripts.Battle.Commands.Implements;
using UnityEngine;

namespace GL.Scripts.Battle.Commands.Blueprints
{
    /// <summary>
    /// パラメータ倍率上昇コマンドの設定データ.
    /// </summary>
    [CreateAssetMenu(menuName = "GL/Commands/Blueprints/AddStatusParameterRate")]
    public sealed class AddStatusParameterRate : Blueprint
    {
        [SerializeField]
        private Implements.AddStatusParameterRate.Parameter parameter;
        
        public override IImplement Create()
        {
            return new Implements.AddStatusParameterRate(this.parameter);
        }
    }
}
