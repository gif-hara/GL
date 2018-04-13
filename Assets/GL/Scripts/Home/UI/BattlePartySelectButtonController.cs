using GL.Scripts.Battle.PartyControllers.Blueprints;
using GL.Scripts.Systems;
using GL.Scripts.User;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
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

        public void Initialize(Blueprint enemyParty)
        {
            this.text.text = enemyParty.PartyName;
            this.button.OnClickAsObservable()
                .Where(_ => this.button.isActiveAndEnabled)
                .SubscribeWithState(enemyParty, (_, e) =>
                {
                    SceneMediator.PlayerParty = UserData.Instance.CurrentParty.AsBlueprint;
                    SceneMediator.EnemyParty = e;
                    SceneManager.LoadScene("Battle");
                })
                .AddTo(this);
        }
    }
}
