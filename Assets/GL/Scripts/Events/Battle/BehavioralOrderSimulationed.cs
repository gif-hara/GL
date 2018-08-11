using System.Collections.Generic;
using GL.Battle.CharacterControllers;
using HK.Framework.EventSystems;

namespace GL.Events.Battle
{
    /// <summary>
    /// 行動順シミュレーション結果を通知するイベント
    /// </summary>
    public sealed class BehavioralOrderSimulationed : Message<BehavioralOrderSimulationed, List<Character>>
    {
        /// <summary>
        /// 行動順
        /// </summary>
        public List<Character> Order{ get{ return this.param1; } }
    }
}
