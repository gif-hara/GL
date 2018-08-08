using System;
using GL.Scripts.Battle.CharacterControllers;

namespace GL.Scripts.Battle.Commands.Element
{
    /// <summary>
    /// 実際にゲームで使用するコマンドのインターフェイス
    /// </summary>
    public interface IImplement
    {
        /// <summary>
        /// ダメージを伴うコマンドであるか
        /// </summary>
        bool TakeDamage { get; }

        /// <summary>
        /// コマンドを実行する
        /// </summary>
        /// <param name="invoker">実行するキャラクター</param>
        void Invoke(Character invoker, Commands.Bundle.Implement bundle, Character[] targets);

        /// <summary>
        /// ターゲットとなるキャラクターのリストを返す
        /// </summary>
        /// <remarks>
        /// <see cref="TargetType"/>が<c>Select</c>の場合はアサートを吐くので注意
        /// </remarks>
        Character[] GetTargets(Character invoker);
    }
}
