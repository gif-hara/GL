using GL.Scripts.Battle.Commands.Implements;
using UnityEngine;

namespace GL.Scripts.Battle.Commands.Blueprints
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
    }
}
