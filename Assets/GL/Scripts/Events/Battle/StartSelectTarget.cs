using GL.Scripts.Battle.CharacterControllers;
using GL.Scripts.Battle.Commands.Implements;
using HK.Framework.EventSystems;

namespace GL.Scripts.Events.Battle
{
    /// <summary>
    /// ターゲットの選択開始を通知するイベント
    /// </summary>
    public sealed class StartSelectTarget : Message<StartSelectTarget, Character, IImplement, Character[]>
    {
        /// <summary>
        /// コマンドを選択中のキャラクター
        /// </summary>
        public Character Character { get { return this.param1; } }

        /// <summary>
        /// 実行するコマンド
        /// </summary>
        public IImplement Command { get { return this.param2; } }

        /// <summary>
        /// 選択可能なターゲットリスト
        /// </summary>
        public Character[] Targets { get { return this.param3; } }
    }
}
