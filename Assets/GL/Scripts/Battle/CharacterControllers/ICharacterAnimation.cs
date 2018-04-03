using System;

namespace GL.Scripts.Battle.CharacterControllers
{
    /// <summary>
    /// キャラクターのアニメーションを制御する
    /// </summary>
    public interface ICharacterAnimation
    {
        /// <summary>
        /// 攻撃アニメーションを開始する
        /// </summary>
        void StartAttack(Action stateExitAction);

        /// <summary>
        /// 死亡アニメーションを開始する
        /// </summary>
        void StartDead(Action stateExitAction);
    }
}
