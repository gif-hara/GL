using System;
using GL.Scripts.User;
using UnityEngine;
using UnityEngine.UI;

namespace GL.Scripts.UI.PopupControllers
{
    /// <summary>
    /// キャラクター詳細ポップアップを制御するクラス
    /// </summary>
    public sealed class CharacterDetailsPopupController : MonoBehaviour
    {
        [SerializeField]
        private Text characterName;

        [SerializeField]
        private Text jobName;

        [SerializeField]
        private Parameter parameter;

        [SerializeField]
        private Resistance resistance;
        
        public void Setup(Player player)
        {
            this.characterName.text = player.PlayerName;
            this.jobName.text = player.Blueprint.Job.JobName;
            this.parameter.Apply(player.Parameter);
            this.resistance.Apply(player.Resistance);
        }

        [Serializable]
        public class Parameter
        {
            [SerializeField]
            private Text hitPoint;

            [SerializeField]
            private Text strength;

            [SerializeField]
            private Text defense;

            [SerializeField]
            private Text sympathy;

            [SerializeField]
            private Text nega;

            [SerializeField]
            private Text speed;

            [SerializeField]
            private Text luck;

            public void Apply(Battle.CharacterControllers.Parameter parameter)
            {
                this.hitPoint.text = parameter.HitPoint.ToString();
                this.strength.text = parameter.Strength.ToString();
                this.defense.text = parameter.Defense.ToString();
                this.sympathy.text = parameter.Sympathy.ToString();
                this.nega.text = parameter.Nega.ToString();
                this.speed.text = parameter.Speed.ToString();
                this.luck.text = parameter.Luck.ToString();
            }
        }

        [Serializable]
        public class Resistance
        {
            [SerializeField]
            private Text poison;

            [SerializeField]
            private Text paralysis;

            [SerializeField]
            private Text sleep;

            [SerializeField]
            private Text confuse;

            [SerializeField]
            private Text berserk;

            [SerializeField]
            private Text vitals;

            public void Apply(Battle.CharacterControllers.Resistance resistance)
            {
                const string format = "P0";
                this.poison.text = resistance.Poison.ToString(format);
                this.paralysis.text = resistance.Paralysis.ToString(format);
                this.sleep.text = resistance.Sleep.ToString(format);
                this.confuse.text = resistance.Confuse.ToString(format);
                this.berserk.text = resistance.Berserk.ToString(format);
                this.vitals.text = resistance.Vitals.ToString(format);
            }
        }
    }
}
