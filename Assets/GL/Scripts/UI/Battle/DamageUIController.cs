using UnityEngine;
using UnityEngine.UI;

namespace GL.Battle.UI
{
    /// <summary>
    /// ダメージUIを制御するクラス
    /// </summary>
    public sealed class DamageUIController : MonoBehaviour
    {
        [SerializeField]
        private Text text;

        [SerializeField]
        private Image background;

        [SerializeField]
        private Color damageColor;

        [SerializeField]
        private Color recoveryColor;

        [SerializeField]
        private float destroyDelay;

        void Start()
        {
            Destroy(this.gameObject, this.destroyDelay);
        }

        public void AsDamage(Transform target, int value)
        {
            this.SetProperty(target, value);
            this.background.color = this.damageColor;
        }
        
        public void AsRecovery(Transform target, int value)
        {
            this.SetProperty(target, value);
            this.background.color = this.recoveryColor;
        }

        private void SetProperty(Transform target, int value)
        {
            this.text.text = value.ToString();
            this.transform.localPosition = Vector3.zero;
        }
    }
}
