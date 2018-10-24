using GL.Battle.PartyControllers;
using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Events.Battle
{
    /// <summary>
    /// パーティの生成が完了した際のイベント
    /// </summary>
    public sealed class CreatedParties : Message<CreatedParties, Parties>
    {
        public Parties Parties => this.param1;
    }
}
