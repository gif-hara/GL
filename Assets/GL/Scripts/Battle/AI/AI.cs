using System;
using GL.Battle.CharacterControllers;
using UnityEngine;
using UnityEngine.Assertions;
using GL.Events.Battle;

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
        private CommandSelector[] defaultCommandSelectors;
        public CommandSelector[] DefaultCommandSelectors => this.defaultCommandSelectors;

        /// <summary>
        /// <see cref="EndTurn"/>で実行されるデフォルトの<see cref="EventSelector"/>
        /// </summary>
        [SerializeField]
        private EventSelector[] defaultOnEndTurnEventSelectors;
        public EventSelector[] DefaultOnEndTurnEventSelectors => this.defaultOnEndTurnEventSelectors;
    }
}
