using System;
using UnityEngine;

namespace GL.Scripts.Battle.CharacterControllers
{
    /// <summary>
    /// キャラクターのステータス
    /// </summary>
    [Serializable]
    public sealed class CharacterStatus
    {
        /// <summary>
        /// キャラクター名
        /// </summary>
        [SerializeField]
        public string Name;

        [SerializeField]
        public Parameter Parameter;

        public CharacterStatus()
        {
            this.Name = "";
            this.Parameter = new Parameter();
        }

        public CharacterStatus(Blueprint blueprint)
        {
            this.Name = blueprint.Status.Name;
            this.Parameter = new Parameter(blueprint);
        }

        [Serializable]
        public class Resistance
        {
            [SerializeField]
            public float Poison;

            [SerializeField]
            public float Paralysis;

            [SerializeField]
            public float Sleep;

            [SerializeField]
            public float Confuse;

            [SerializeField]
            public float Berserk;
        }
    }
}
