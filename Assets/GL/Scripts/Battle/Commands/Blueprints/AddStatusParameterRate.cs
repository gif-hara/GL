using GL.Scripts.Battle.Commands.Impletents;
using GL.Scripts.Battle.Systems;
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
        private Constants.StatusParameterType statusParameterType;

        [SerializeField]
        private float rate;
        
        public override IImplement Create()
        {
            return new Impletents.AddStatusParameterRate(
                this.commandName.Get,
                this.targetPartyType,
                this.targetType,
                this.statusParameterType,
                this.rate
                );
        }
    }
}
