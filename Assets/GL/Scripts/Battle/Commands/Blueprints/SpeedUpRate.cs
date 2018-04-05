using GL.Scripts.Battle.Commands.Impletents;
using UnityEngine;

namespace GL.Scripts.Battle.Commands.Blueprints
{
    /// <summary>
    /// 素早さ倍率上昇コマンドの設定データ.
    /// </summary>
    [CreateAssetMenu(menuName = "GL/Commands/Blueprints/SpeedUpRate")]
    public sealed class SpeedUpRate : Blueprint
    {
        public override IImplement Create()
        {
            return new Impletents.SpeedUpRate(this.commandName.Get, this.targetType);
        }
    }
}
