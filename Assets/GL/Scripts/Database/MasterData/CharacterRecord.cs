﻿using System;
using System.Linq;
using System.Text;
using GL.Battle.AI;
using GL.Battle.CharacterControllers;
using GL.Battle.CharacterControllers.JobSystems;
using HK.Framework.Text;
using HK.GL.Extensions;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace GL.Database
{
    /// <summary>
    /// キャラクターレコード
    /// </summary>
    [CreateAssetMenu(menuName = "GL/MasterData/Record/Character")]
    public sealed class CharacterRecord : ScriptableObject, IMasterDataRecord
    {
        public string Id { get { return this.name; } }

        [SerializeField]
        private Sprite icon;
        public Sprite Icon => this.icon;

        [SerializeField]
        private Color iconColor;
        public Color IconColor => this.iconColor;

        [SerializeField]
        private int rank;
        /// <summary>
        /// ランク
        /// </summary>
        /// <remarks>
        /// 装備出来る武器の条件になる
        /// </remarks>
        public int Rank => this.rank;

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
        private Battle.CharacterControllers.Attribute attribute;
        public Battle.CharacterControllers.Attribute Attribute => this.attribute;

        [SerializeField]
        private GrowthCurve growthCurve;

        [SerializeField]
        private ExperienceData experience;
        public ExperienceData Experience => this.experience;

        [SerializeField]
        private int price;
        public int Price => this.price;

        [SerializeField]
        private int acquireExperience;
        /// <summary>
        /// 獲得できる経験値
        /// </summary>
        /// <remarks>
        /// 敵を倒した際に加算するのに利用しています
        /// </remarks>
        public int AcquireExperience => this.acquireExperience;

        [SerializeField]
        private MaterialLottery[] materialLotteries;
        public MaterialLottery[] MaterialLotteries => this.materialLotteries;

        [SerializeField]
        private AIController aiController;
        public AIController AIController => this.aiController;

#if UNITY_EDITOR
        [ContextMenu("OutputGrowth All")]
        private void OutputGrowthAll()
        {
            var parameter = string.Join(
                System.Environment.NewLine,
                this.GetLevelAllParameter().Select(p => p.ToCSV())
                );
            EditorGUIUtility.systemCopyBuffer = parameter;
        }

        private Parameter[] GetLevelAllParameter()
        {
            const int min = Constants.LevelMin;
            const int max = Constants.LevelMax;
            var parameters = new Parameter[max];
            for (int i = min; i <= max; i++)
            {
                parameters[i - 1] = this.GetParameter(i);
            }

            return parameters;
        }
#endif

        public void ApplyIcon(Image image)
        {
            image.sprite = this.icon;
            image.color = this.iconColor;
        }

        public Parameter GetParameter(int level)
        {
            return this.EvaluteParameter(level);
        }

        private Parameter EvaluteParameter(int level)
        {
            var t = (float)(level - 1) / (Constants.LevelMax - 1);
            return new Parameter
            {
                HitPoint = Mathf.FloorToInt(Mathf.Lerp(this.min.HitPoint, this.max.HitPoint, this.growthCurve.HitPoint.Evaluate(t))),
                Strength = Mathf.FloorToInt(Mathf.Lerp(this.min.Strength, this.max.Strength, this.growthCurve.Strength.Evaluate(t))),
                StrengthMagic = Mathf.FloorToInt(Mathf.Lerp(this.min.StrengthMagic, this.max.StrengthMagic, this.growthCurve.StrengthMagic.Evaluate(t))),
                Defense = Mathf.FloorToInt(Mathf.Lerp(this.min.Defense, this.max.Defense, this.growthCurve.Defense.Evaluate(t))),
                DefenseMagic = Mathf.FloorToInt(Mathf.Lerp(this.min.DefenseMagic, this.max.DefenseMagic, this.growthCurve.DefenseMagic.Evaluate(t))),
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

        [Serializable]
        public class ExperienceData
        {
            [SerializeField]
            private int max;

            [SerializeField]
            private AnimationCurve curve;

            public int GetNeedValue(int level)
            {
                var n = (float)level / Constants.LevelMax;
                var o = (float)(level - 1) / Constants.LevelMax;
                var next = Mathf.FloorToInt(Mathf.Lerp(0, this.max, this.curve.Evaluate(n)));
                var old = Mathf.FloorToInt(Mathf.Lerp(0, this.max, this.curve.Evaluate(o)));

                return next - old;
            }
        }

        [Serializable]
        public class MaterialLottery
        {
            [SerializeField]
            private MaterialRecord material;
            public MaterialRecord Material => this.material;

            [SerializeField][Range(0.0f, 1.0f)]
            private float lottery;

            /// <summary>
            /// 獲得可能か返す
            /// </summary>
            public bool IsAcquire => UnityEngine.Random.value <= this.lottery;
        }
    }
}
