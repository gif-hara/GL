using GL.Scripts.Battle.Systems;
using HK.Framework.EventSystems;

namespace HK.GL.Events.Battle
{
    /// <summary>
    /// バトル終了を通知するイベント
    /// </summary>
    public sealed class EndBattle : Message<EndBattle, Constants.BattleResult>
    {
        public Constants.BattleResult Result{ get{ return this.param1; } }
    }
}
