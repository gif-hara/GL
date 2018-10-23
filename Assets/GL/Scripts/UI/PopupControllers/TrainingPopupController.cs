using GL.Events.Home;
using GL.User;
using HK.Framework.EventSystems;
using HK.Framework.Text;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace GL.UI.PopupControllers
{
    /// <summary>
    /// トレーニングポップアップを制御するクラス
    /// </summary>
    public sealed class TrainingPopupController : PopupBase
    {
        public enum SubmitType
        {
            LevelUp,
            Cancel,
        }

        [SerializeField]
        private Text possessionExperience;

        [SerializeField]
        private Text needExperience;

        [SerializeField]
        private Button decideButton;

        [SerializeField]
        private Button cancelButton;

        [SerializeField]
        private StringAsset.Finder levelMax;

        public TrainingPopupController Setup(Player player)
        {
            this.SetupInternal(player);
            this.decideButton.OnClickAsObservable().SubscribeWithState(this, (_, _this) => _this.submit.OnNext((int)SubmitType.LevelUp));
            this.cancelButton.OnClickAsObservable().SubscribeWithState(this, (_, _this) => _this.submit.OnNext((int)SubmitType.Cancel));

            Broker.Global.Receive<LevelUppedPlayer>()
                .SubscribeWithState(this, (x, _this) => _this.SetupInternal(x.Player))
                .AddTo(this);

            return this;
        }

        private void SetupInternal(Player player)
        {
            var isLevelMax = player.IsLevelMax;
            this.possessionExperience.text = UserData.Instance.Wallet.Experience.Value.ToString();
            this.needExperience.text = isLevelMax ? this.levelMax.Get : player.CharacterRecord.Experience.GetNeedValue(player.Level + 1).ToString();
            this.decideButton.interactable = !isLevelMax;
        }
    }
}
