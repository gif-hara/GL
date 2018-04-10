using GL.Scripts.Battle.PartyControllers.Blueprints;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace GL.Scripts.Home.UI
{
    /// <summary>
    /// バトルする敵パーティを選択するボタンを制御するクラス
    /// </summary>
    public sealed class BattlePartySelectButtonController : MonoBehaviour
    {
        [SerializeField]
        private Button button;
        
        [SerializeField]
        private Text text;

        public void Initialize(Blueprint blueprint)
        {
            this.text.text = blueprint.PartyName;
            this.button.OnClickAsObservable()
                .Where(_ => this.button.isActiveAndEnabled)
                .SubscribeWithState(this, (_, _this) =>
                {
                    Debug.Log("Battle! " + _this.text.text);
                })
                .AddTo(this);
        }
    }
}
