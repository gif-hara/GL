using System;
using GL.Scripts.Battle.CharacterControllers.Blueprints;
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

        [SerializeField]
        public Resistance Resistance;

        public CharacterStatus()
        {
            this.Name = "";
            this.Parameter = new Parameter();
            this.Resistance = new Resistance();
        }

        public CharacterStatus(Blueprint blueprint)
        {
            this.Name = blueprint.CharacterName;
            this.Parameter = new Parameter(blueprint);
            this.Resistance = new Resistance(blueprint);
        }

        public void Copy(CharacterStatus other)
        {
            this.Name = other.Name;
            this.Parameter = new Parameter(other.Parameter);
            this.Resistance = new Resistance(other.Resistance);
        }

        public void Reset()
        {
            this.Name = "";
            this.Parameter.Reset();
            this.Resistance.Reset();
        }
    }
}
