using System;
using UnityEngine;

namespace GL.Battle.Commands.Element.Blueprints
{
    /// <summary>
    /// 状態異常を付与するコマンドの設定データ.
    /// </summary>
    [CreateAssetMenu(menuName = "GL/Commands/Blueprints/AddStatusAilment")]
    public sealed class AddStatusAilment : Blueprint
    {
        [SerializeField]
        private Implements.AddStatusAilment.Parameter parameter;
        
        public override IImplement Create()
        {
            return new Implements.AddStatusAilment(this.parameter);
        }

#if UNITY_EDITOR
        public override string FileName => $"AddStatusAilment_{Enum.GetName(typeof(Constants.StatusAilmentType), this.parameter.StatusAilmentType)}_{this.parameter.Rate}_{this.parameter.RemainingTurn}";
#endif
    }
}
