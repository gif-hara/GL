using System;
using GL.Battle.CharacterControllers;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace GL.Battle.UI
{
    /// <summary>
    /// キャラクターUIを制御するクラス
    /// </summary>
    public sealed class CharacterUIController : MonoBehaviour
    {
        [SerializeField]
        private Color playerColor;

        [SerializeField]
        private Color enemyColor;

        [SerializeField]
        private Image icon;

        [SerializeField]
        private Image background;

        [SerializeField]
        private HitPoint hitPoint;

        [SerializeField]
        private Status status;

        private Character character;

        public void Setup(Character character)
        {
            this.character = character;
            Assert.IsNotNull(this.character);

            var isOpponent = this.character.CharacterType == Constants.CharacterType.Enemy;

            this.background.color = isOpponent ? this.enemyColor : this.playerColor;
            this.hitPoint.Apply(this.character);
            this.status.Apply(this.character);

            if(isOpponent)
            {
                // 相手側の場合は子要素を逆転させる
                var count = this.transform.childCount;
                var children = new Transform[count];
                for (var i = 0; i < count; ++i)
                {
                    children[i] = this.transform.GetChild(i);
                }

                for (var i = 0; i < count; ++i)
                {
                    children[count - 1 - i].SetAsLastSibling();
                }
            }
        }

        [Serializable]
        public class HitPoint
        {
            [SerializeField]
            private GameObject root;
            public GameObject Root => this.root;

            [SerializeField]
            private Text text;

            [SerializeField]
            private Scrollbar gauge;

            public void Apply(Character character)
            {
                this.text.text = character.StatusController.HitPoint.ToString();
                this.gauge.size = character.StatusController.HitPointRate;
            }
        }

        [Serializable]
        public class Status
        {
            [SerializeField]
            private GameObject root;
            public GameObject Root => this.root;

            [SerializeField]
            private Text strength;

            [SerializeField]
            private Text strengthMagic;

            [SerializeField]
            private Text defense;

            [SerializeField]
            private Text defenseMagic;

            [SerializeField]
            private Text speed;

            public void Apply(Character character)
            {
                this.strength.text = character.StatusController.GetTotalParameter(Constants.StatusParameterType.Strength).ToString();
                this.strengthMagic.text = character.StatusController.GetTotalParameter(Constants.StatusParameterType.StrengthMagic).ToString();
                this.defense.text = character.StatusController.GetTotalParameter(Constants.StatusParameterType.Defense).ToString();
                this.defenseMagic.text = character.StatusController.GetTotalParameter(Constants.StatusParameterType.DefenseMagic).ToString();
                this.speed.text = character.StatusController.GetTotalParameter(Constants.StatusParameterType.Speed).ToString();
            }
        }
    }
}
