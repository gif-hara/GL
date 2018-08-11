using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace GL.UI.PopupControllers
{
    /// <summary>
    /// ポップアップを閉じるボタン
    /// </summary>
    public sealed class ClosePopupButtonController : MonoBehaviour
    {
        [SerializeField]
        private Button button;

        void Awake()
        {
            this.button.OnClickAsObservable()
                .Where(_ => this.button.isActiveAndEnabled)
                .Subscribe(_ => PopupController.Instance.Close())
                .AddTo(this);
        }
    }
}
