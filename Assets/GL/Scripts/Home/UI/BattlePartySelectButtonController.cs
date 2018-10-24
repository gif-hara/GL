using GL.Database;
using GL.Systems;
using GL.User;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GL.Home.UI
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

        public void Initialize(PartyRecord enemyParty)
        {
            this.text.text = enemyParty.PartyName;
            this.button.OnClickAsObservable()
                .Where(_ => this.button.isActiveAndEnabled)
                .SubscribeWithState(enemyParty, (_, e) =>
                {
                    SceneMediator.PlayerParty = UserData.Instance.CurrentParty.AsPartyRecord;
                    SceneMediator.EnemyParty = e;
                    SceneManager.LoadScene("Battle");
                })
                .AddTo(this);
        }
    }
}
