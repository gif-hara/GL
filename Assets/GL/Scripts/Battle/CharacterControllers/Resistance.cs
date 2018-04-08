using System;
using GL.Scripts.Battle.Systems;
using GL.Scripts.Extensions;
using UnityEngine;
using UnityEngine.Assertions;

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

        public float Get(Constants.StatusAilmentType statusAilmentType)
        {
            Assert.IsTrue(statusAilmentType.IsNegative(), string.Format("{0}はNegativeではありません", statusAilmentType));
            switch (statusAilmentType)
            {
                case Constants.StatusAilmentType.Poison:
                    return this.Poison;
                case Constants.StatusAilmentType.Paralysis:
                    return this.Paralysis;
                case Constants.StatusAilmentType.Sleep:
                    return this.Sleep;
                case Constants.StatusAilmentType.Confuse:
                    return this.Confuse;
                case Constants.StatusAilmentType.Berserk:
                    return this.Berserk;
                default:
                    Assert.IsTrue(false, string.Format("未対応の値です {0}", statusAilmentType));
                    return 0.0f;
            }
        }
    }
}
