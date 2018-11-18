using System;
using GL.Battle.CharacterControllers;
using UnityEngine;
using UnityEngine.Assertions;
using GL.Events.Battle;
using System.Collections.Generic;

namespace GL.Battle.AIControllers
{
    /// <summary>
    /// AIに必要な要素を持つクラス
    /// </summary>
    [CreateAssetMenu(menuName = "GL/AI/AI")]
    public sealed class AI : ScriptableObject
    {
        /// <summary>
        /// デフォルトの<see cref="CommandSelector"/>
        /// </summary>
        [SerializeField]
        private CommandSelectorList[] commandSelectorLists;
        public CommandSelectorList[] CommandSelectorLists => this.commandSelectorLists;

        /// <summary>
        /// <see cref="EndTurn"/>で実行されるデフォルトの<see cref="EventSelector"/>
        /// </summary>
        [SerializeField]
        private EventSelectorList[] onEndTurnEventSelectorLists;
        public EventSelectorList[] OnEndTurnEventSelectorLists => this.onEndTurnEventSelectorLists;

        [Serializable]
        public class CommandSelectorList
        {
            [SerializeField]
            private List<CommandSelector> commandSelectors;
            public List<CommandSelector> CommandSelectors => this.commandSelectors;

            public CommandSelectorList()
            {
                this.commandSelectors = new List<CommandSelector>();
            }
        }

        [Serializable]
        public class EventSelectorList
        {
            [SerializeField]
            private List<EventSelector> eventSelectors;
            public List<EventSelector> EventSelectors => this.eventSelectors;

            public EventSelectorList()
            {
                this.eventSelectors = new List<EventSelector>();
            }
        }

#if UNITY_EDITOR
        public AI Set(CommandSelectorList[] commandSelectorLists, EventSelectorList[] onEndTurnEventSelectorLists)
        {
            this.commandSelectorLists = commandSelectorLists;
            this.onEndTurnEventSelectorLists = onEndTurnEventSelectorLists;

            return this;
        }
#endif
    }
}
