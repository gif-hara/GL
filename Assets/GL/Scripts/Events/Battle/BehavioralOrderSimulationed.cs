using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.GL.Battle;

namespace HK.GL.Events.Battle
{
    /// <summary>
    /// 行動順シミュレーション結果を通知するイベント
    /// </summary>
    public sealed class BehavioralOrderSimulationed : GLEvent<BehavioralOrderSimulationed, List<Character>>
    {
        /// <summary>
        /// 行動順
        /// </summary>
        public List<Character> Order{ get{ return this.param1; } }
    }
}
