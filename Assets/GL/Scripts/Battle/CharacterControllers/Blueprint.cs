using System;
using GL.Battle;
using GL.Battle.CharacterControllers.JobSystems;
using HK.Framework.Text;
using UnityEngine;

namespace GL.Battle.CharacterControllers
{
    /// <summary>
    /// キャラクターを構成する設計図
    /// </summary>
    [CreateAssetMenu(menuName = "GL/CharacterControllers/Blueprint")]
    public sealed class Blueprint : ScriptableObject
    {
        public string Id { get { return this.name; } }
        
        [SerializeField]
        private StringAsset.Finder characterName;
        public string CharacterName { get { return characterName.Get; } }
        
        [SerializeField]
        private Job job;
        public Job Job { get { return job; } }
        
        [SerializeField]
        private GameObject model;
        public GameObject Model { get { return this.model; } }

        [SerializeField]
        private Parameter min;
        public Parameter Min => this.min;

        [SerializeField]
        private Parameter max;
        public Parameter Max => this.max;

        [SerializeField]
        private Resistance resistance;
        public Resistance Resistance => this.resistance;

        [SerializeField]
        private GrowthCurve growthCurve;

        [SerializeField]
        private int price;
        public int Price => this.price;

        public Parameter GetParameter(int level)
        {
            return this.EvaluteParameter(level);
        }

        private Parameter EvaluteParameter(int level)
        {
            var t = (float)level / (Constants.LevelMax - Constants.LevelMin);
            return new Parameter
            {
                HitPoint = Mathf.FloorToInt(Mathf.Lerp(this.min.HitPoint, this.max.HitPoint, this.growthCurve.HitPoint.Evaluate(t))),
                Strength = Mathf.FloorToInt(Mathf.Lerp(this.min.Strength, this.max.Strength, this.growthCurve.Strength.Evaluate(t))),
                Defense = Mathf.FloorToInt(Mathf.Lerp(this.min.Defense, this.max.Defense, this.growthCurve.Defense.Evaluate(t))),
                Speed = Mathf.FloorToInt(Mathf.Lerp(this.min.Speed, this.max.Speed, this.growthCurve.Speed.Evaluate(t))),
            };
        }

        [Serializable]
        public class GrowthCurve
        {
            public AnimationCurve HitPoint;
            public AnimationCurve Strength;
            public AnimationCurve StrengthMagic;
            public AnimationCurve Defense;
            public AnimationCurve DefenseMagic;
            public AnimationCurve Speed;
        }
    }
}
