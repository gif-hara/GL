using GL.Scripts.Battle.Commands.Impletents;
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
        private Impletents.AddStatusAilment.AddStatusAilmentParameter parameter;

        public override IImplement Create()
        {
            return new Impletents.AddStatusAilment(this.parameter);
        }
    }
}
