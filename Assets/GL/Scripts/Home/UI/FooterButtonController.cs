using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace GL.Home.UI
{
    /// <summary>
    /// フッターボタンを制御するクラス
    /// </summary>
    public sealed class FooterButtonController : MonoBehaviour
    {
        [SerializeField]
        private MovableAreaController movableAreaController;

        [SerializeField]
        private int rootIndex;
        
        void Awake()
        {
            var button = this.GetComponent<Button>();
            button.OnClickAsObservable()
                .Where(_ => button.isActiveAndEnabled)
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.movableAreaController.ChangeRoot(_this.rootIndex);
                })
                .AddTo(this);
        }
    }
}
