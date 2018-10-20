using System;
using UnityEngine;

namespace GL.Battle.Commands.Element.Blueprints
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

#if UNITY_EDITOR
        public override string FileName => $"AddStatusParameterRate_{Enum.GetName(typeof(Constants.StatusParameterType), this.parameter.ParameterType)}_{this.parameter.Rate}";
#endif
    }
}
