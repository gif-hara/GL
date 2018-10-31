using System;
using System.Linq;
using System.Text;
using GL.Battle.AI;
using GL.Battle.CharacterControllers;
using GL.Battle.CharacterControllers.JobSystems;
using HK.Framework.Text;
using HK.GL.Extensions;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
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

        /// <summary>
        /// レベル
        /// </summary>
        /// <remarks>
        /// 敵のみ利用しています
        /// </remarks>
        [SerializeField]
        private int level;
        public int Level => this.level;

#if UNITY_EDITOR
        [ContextMenu("OutputGrowth All")]
        private void OutputGrowthAll()
        {
            var parameters = this.GetLevelAllParameter();
            var result = new StringBuilder();
            for (var i = 1; i <= Constants.LevelMax; i++)
            {
                var l = new List<int>();
                var p = parameters[i - 1];
                l.Add(i);
                l.Add(this.Experience.GetNeedValue(i));
                l.AddRange(p.ToArray());

                result.AppendLine(string.Join(",", l));
            }
            EditorGUIUtility.systemCopyBuffer = result.ToString();
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

        public void SetAsEnemy(
            Sprite icon,
            Color iconColor,
            int rank,
            StringAsset.Finder characterName,
            Job job,
            Parameter min,
            Parameter max,
            Resistance resistance,
            Battle.CharacterControllers.Attribute attribute,
            GrowthCurve growthCurve,
            ExperienceData experience,
            int price,
            int acquireExperience,
            MaterialLottery[] materialLotteries,
            int level
        )
        {
            this.icon = icon;
            this.iconColor = iconColor;
            this.rank = rank;
            this.characterName = characterName;
            this.job = job;
            this.min = min;
            this.max = max;
            this.resistance = resistance;
            this.attribute = attribute;
            this.growthCurve = growthCurve;
            this.experience = experience;
            this.price = price;
            this.acquireExperience = acquireExperience;
            this.materialLotteries = materialLotteries;
            // AIControllerは手動で付ける予定
            this.level = level;
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
                Critical = Mathf.FloorToInt(Mathf.Lerp(this.min.Critical, this.max.Critical, this.growthCurve.Critical.Evaluate(t))),
                Avoidance = Mathf.FloorToInt(Mathf.Lerp(this.min.Avoidance, this.max.Avoidance, this.growthCurve.Avoidance.Evaluate(t))),
            };
        }

        [Serializable]
        public class GrowthCurve
        {
            public AnimationCurve HitPoint = new AnimationCurve();
            public AnimationCurve Strength = new AnimationCurve();
            public AnimationCurve StrengthMagic = new AnimationCurve();
            public AnimationCurve Defense = new AnimationCurve();
            public AnimationCurve DefenseMagic = new AnimationCurve();
            public AnimationCurve Speed = new AnimationCurve();
            public AnimationCurve Critical = new AnimationCurve();
            public AnimationCurve Avoidance = new AnimationCurve();
        }

        [Serializable]
        public class ExperienceData
        {
            [SerializeField]
            private int max;

            [SerializeField]
            private AnimationCurve curve = new AnimationCurve();

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

            public MaterialLottery(MaterialRecord material, float lottery)
            {
                this.material = material;
                this.lottery = lottery;
            }
        }
    }
}
