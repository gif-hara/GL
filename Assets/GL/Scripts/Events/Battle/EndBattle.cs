using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.GL.Battle;

namespace HK.GL.Events.Battle
{
    /// <summary>
    /// バトル終了を通知するイベント
    /// </summary>
    public sealed class EndBattle : GLEvent<EndBattle, Constants.BattleResult>
    {
        public Constants.BattleResult Result{ get{ return this.param1; } }
    }
}
