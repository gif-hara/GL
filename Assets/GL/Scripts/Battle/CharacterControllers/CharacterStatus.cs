using System;
using GL.Database;
using UnityEngine;

namespace GL.Battle.CharacterControllers
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
        public CharacterParameter Parameter;

        [SerializeField]
        public Resistance Resistance;

        [SerializeField]
        public Attribute Attribute;

        public CharacterStatus()
        {
            this.Name = "";
            this.Parameter = new CharacterParameter();
            this.Resistance = new Resistance();
            this.Attribute = new Attribute();
        }

        public CharacterStatus(CharacterRecord characterRecord, int level)
        {
            this.Name = characterRecord.CharacterName;
            this.Parameter = new CharacterParameter(characterRecord, level);
            this.Resistance = new Resistance(characterRecord);
            this.Attribute = new Attribute(characterRecord);
        }

        public void Copy(CharacterStatus other)
        {
            this.Name = other.Name;
            this.Parameter = new CharacterParameter(other.Parameter);
            this.Resistance = new Resistance(other.Resistance);
            this.Attribute = new Attribute(other.Attribute);
        }

        public void Reset()
        {
            this.Name = "";
            this.Parameter.Reset();
            this.Resistance.Reset();
            this.Attribute.Reset();
        }
    }
}
