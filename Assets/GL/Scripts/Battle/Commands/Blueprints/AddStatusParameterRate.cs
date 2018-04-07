using GL.Scripts.Battle.Commands.Impletents;
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
        private Impletents.AddStatusParameterRate.AddStatusParameterRateParameter parameter;
        
        public override IImplement Create()
        {
            return new Impletents.AddStatusParameterRate(this.parameter);
        }
    }
}
