using System;
using UnityEngine;

namespace GL.Scripts.Battle.CharacterControllers
{
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

        public Resistance()
        {
            this.Poison = 0.0f;
            this.Paralysis = 0.0f;
            this.Sleep = 0.0f;
            this.Confuse = 0.0f;
            this.Berserk = 0.0f;
        }

        public Resistance(Blueprint blueprint)
        {
            this.Poison = blueprint.Status.Resistance.Poison;
            this.Paralysis = blueprint.Status.Resistance.Paralysis;
            this.Sleep = blueprint.Status.Resistance.Sleep;
            this.Confuse = blueprint.Status.Resistance.Confuse;
            this.Berserk = blueprint.Status.Resistance.Berserk;
        }
    }
}
