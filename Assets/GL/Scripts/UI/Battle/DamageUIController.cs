using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using UnityEngine.UI;

namespace HK.GL.UI.Battle
{
    /// <summary>
    /// ダメージUIを制御するヤーツ
    /// </summary>
    public sealed class DamageUIController : MonoBehaviour
    {
        [SerializeField]
        private Text text;

        [SerializeField]
        private float destroyDelay;

        void Start()
        {
            Destroy(this.gameObject, this.destroyDelay);
        }

        public void SetProperty(Transform target, int value)
        {
            this.text.text = value.ToString();
            this.transform.position = Camera.main.WorldToScreenPoint(target.position);
        }
    }
}
