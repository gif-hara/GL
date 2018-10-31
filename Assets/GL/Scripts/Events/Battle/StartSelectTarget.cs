using GL.Battle.CharacterControllers;
using GL.Battle.Commands.Bundle;
using HK.Framework.EventSystems;

namespace GL.Events.Battle
{
    /// <summary>
    /// ターゲットの選択開始を通知するイベント
    /// </summary>
    public sealed class StartSelectTarget : Message<StartSelectTarget, Character, Implement, Character[]>
    {
        /// <summary>
        /// コマンドを選択中のキャラクター
        /// </summary>
        public Character Character => this.param1;

        /// <summary>
        /// 実行するコマンド
        /// </summary>
        public Implement Command => this.param2;

        /// <summary>
        /// 選択可能なターゲットリスト
        /// </summary>
        public Character[] Targets => this.param3;
    }
}
