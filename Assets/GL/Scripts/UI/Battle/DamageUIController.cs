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
        private float destroyDelay;

        void Start()
        {
            Destroy(this.gameObject, this.destroyDelay);
        }

        public void SetProperty(Transform target, int value, RectTransform canvasTransform, Camera uiCamera, Camera worldCamera)
        {
            this.text.text = value.ToString();

            Vector2 localPoint;
            var screenPosition = RectTransformUtility.WorldToScreenPoint(worldCamera, target.position);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasTransform, screenPosition, uiCamera, out localPoint);
            this.transform.localPosition = localPoint;
        }
    }
}
