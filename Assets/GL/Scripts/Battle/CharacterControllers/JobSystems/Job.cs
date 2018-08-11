﻿using System;
using GL.Battle;
using HK.Framework.Text;
using UnityEngine;

namespace GL.Battle.CharacterControllers.JobSystems
{
    /// <summary>
    /// 職業
    /// </summary>
    [CreateAssetMenu(menuName = "GL/CharacterControllers/Job")]
    public sealed class Job : ScriptableObject
    {
        [SerializeField]
        private StringAsset.Finder jobName;
        
        [SerializeField]
        private Parameter min;

        [SerializeField]
        private Parameter max;

        [SerializeField]
        private Resistance resistance;

        [SerializeField][EnumFlags]
        private Constants.WeaponType equipable;

        [SerializeField]
        private GrowthCurve growthCurve;
        
        public string JobName { get { return jobName.Get; } }

        public Parameter EvaluteParameter(int level)
        {
            var t = (float) level / (Constants.LevelMax - Constants.LevelMin);
            var result = new Parameter
            {
                HitPoint = Mathf.FloorToInt(Mathf.Lerp(this.min.HitPoint, this.max.HitPoint, this.growthCurve.HitPoint.Evaluate(t))),
                Strength = Mathf.FloorToInt(Mathf.Lerp(this.min.Strength, this.max.Strength, this.growthCurve.Strength.Evaluate(t))),
                Defense = Mathf.FloorToInt(Mathf.Lerp(this.min.Defense, this.max.Defense, this.growthCurve.Defense.Evaluate(t))),
                Speed = Mathf.FloorToInt(Mathf.Lerp(this.min.Speed, this.max.Speed, this.growthCurve.Speed.Evaluate(t))),
                Luck = Mathf.FloorToInt(Mathf.Lerp(this.min.Luck, this.max.Luck, this.growthCurve.Luck.Evaluate(t)))
            };

            return result;
        }
        
        public Resistance Resistance { get { return resistance; } }

        [Serializable]
        public class GrowthCurve
        {
            public AnimationCurve HitPoint;
            public AnimationCurve Strength;
            public AnimationCurve Defense;
            public AnimationCurve Speed;
            public AnimationCurve Luck;
        }
    }
}
