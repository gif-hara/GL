using System;
using GL.Battle.CharacterControllers;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Battle.AIControllers
{
    /// <summary>
    /// コマンドを選択するクラス
    /// </summary>
    [Serializable]
    public sealed class CommandSelector
    {
        [SerializeField]
        private Conditions[] conditions;
        public Conditions[] Conditions => this.conditions;

        [SerializeField]
        private InvokeCommand element;
        public InvokeCommand Element => this.element;

        public bool Suitable(Character invoker)
        {
            foreach (var c in this.conditions)
            {
                if (c.Suitable(invoker))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
