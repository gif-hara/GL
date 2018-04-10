using UnityEngine;
using UnityEngine.UI;

namespace HK.GL.UI.Battle
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

        public void AsDamage(Transform target, int value, RectTransform canvasTransform, Camera uiCamera, Camera worldCamera)
        {
            this.SetProperty(target, value, canvasTransform, uiCamera, worldCamera);
            this.background.color = this.damageColor;
        }
        
        public void AsRecovery(Transform target, int value, RectTransform canvasTransform, Camera uiCamera, Camera worldCamera)
        {
            this.SetProperty(target, value, canvasTransform, uiCamera, worldCamera);
            this.background.color = this.recoveryColor;
        }

        private void SetProperty(Transform target, int value, RectTransform canvasTransform, Camera uiCamera, Camera worldCamera)
        {
            this.text.text = value.ToString();

            Vector2 localPoint;
            var screenPosition = RectTransformUtility.WorldToScreenPoint(worldCamera, target.position);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasTransform, screenPosition, uiCamera, out localPoint);
            this.transform.localPosition = localPoint;
        }
    }
}
