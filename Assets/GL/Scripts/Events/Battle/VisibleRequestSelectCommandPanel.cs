using GL.Battle.CharacterControllers;
using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Events.Battle
{
    /// <summary>
    /// コマンド選択パネルの表示リクエストイベント
    /// </summary>
    public sealed class VisibleRequestSelectCommandPanel : Message<VisibleRequestSelectCommandPanel, Character>
    {
        public Character Character => this.param1;
    }
}
