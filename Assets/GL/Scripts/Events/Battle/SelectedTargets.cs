using GL.Scripts.Battle.CharacterControllers;
using GL.Scripts.Battle.Commands.Implements;
using HK.Framework.EventSystems;

namespace GL.Scripts.Events.Battle
{
    /// <summary>
    /// コマンドを選択した事を通知するイベント
    /// </summary>
    public sealed class SelectedTargets : Message<SelectedTargets, Character, IImplement, Character[]>
    {
        /// <summary>
        /// 実行するキャラクター
        /// </summary>
        public Character Invoker { get{ return this.param1; } }

        /// <summary>
        /// 実行するコマンド
        /// </summary>
        public IImplement Command { get { return this.param2; } }

        /// <summary>
        /// 実行するコマンド
        /// </summary>
        public Character[] Targets { get{ return this.param3; } }
    }
}
