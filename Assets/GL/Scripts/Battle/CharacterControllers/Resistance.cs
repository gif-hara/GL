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

        [SerializeField]
        public float Vitals;

        public Resistance()
        {
            this.Poison = 0.0f;
            this.Paralysis = 0.0f;
            this.Sleep = 0.0f;
            this.Confuse = 0.0f;
            this.Berserk = 0.0f;
            this.Vitals = 0.0f;
        }

        public Resistance(Blueprint blueprint)
        {
            var r = blueprint.Status.Resistance;
            this.Poison = r.Poison;
            this.Paralysis = r.Paralysis;
            this.Sleep = r.Sleep;
            this.Confuse = r.Confuse;
            this.Berserk = r.Berserk;
            this.Vitals = r.Vitals;
        }

        public void Add(Constants.StatusAilmentType statusAilmentType, float value)
        {
            Assert.IsTrue(statusAilmentType.IsNegative(), string.Format("{0}はNegativeではありません", statusAilmentType));
            switch (statusAilmentType)
            {
                case Constants.StatusAilmentType.Poison:
                    this.Poison += value;
                    break;
                case Constants.StatusAilmentType.Paralysis:
                    this.Paralysis += value;
                    break;
                case Constants.StatusAilmentType.Sleep:
                    this.Sleep += value;
                    break;
                case Constants.StatusAilmentType.Confuse:
                    this.Confuse += value;
                    break;
                case Constants.StatusAilmentType.Berserk:
                    this.Berserk += value;
                    break;
                case Constants.StatusAilmentType.Vitals:
                    this.Vitals += value;
                    break;
                default:
                    Assert.IsTrue(false, string.Format("未対応の値です {0}", statusAilmentType));
                    break;
            }
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
                case Constants.StatusAilmentType.Vitals:
                    return this.Vitals;
                default:
                    Assert.IsTrue(false, string.Format("未対応の値です {0}", statusAilmentType));
                    return 0.0f;
            }
        }
    }
}
