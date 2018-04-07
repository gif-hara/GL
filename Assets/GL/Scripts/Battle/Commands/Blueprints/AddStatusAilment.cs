using GL.Scripts.Battle.Commands.Impletents;
using GL.Scripts.Battle.Systems;
using UnityEngine;

namespace GL.Scripts.Battle.Commands.Blueprints
{
    /// <summary>
    /// パラメータ倍率上昇コマンドの設定データ.
    /// </summary>
    [CreateAssetMenu(menuName = "GL/Commands/Blueprints/AddStatusAilment")]
    public sealed class AddStatusAilment : Blueprint
    {
        [SerializeField]
        private Constants.StatusAilmentType type;

        /// <summary>
        /// 状態異常にかかる確率
        /// </summary>
        [SerializeField]
        private float rate;
        
        [SerializeField]
        private int remainingTurnMin;

        [SerializeField]
        private int remainingTurnMax;

        public override IImplement Create()
        {
            return new Impletents.AddStatusAilment(
                this.commandName.Get,
                this.targetPartyType,
                this.targetType,
                this.type,
                this.rate,
                this.remainingTurnMin,
                this.remainingTurnMax
                );
        }
    }
}
