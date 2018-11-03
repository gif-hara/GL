using System.Linq;
using GL.UI;
using GL.UI.PopupControllers;
using GL.User;
using HK.GL.Extensions;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace GL.UI.PopupControllers
{
    /// <summary>
    /// アクセサリー変更のポップアップを制御するクラス
    /// </summary>
    /// <remarks>
    /// <see cref="this.submit"/>は装備したいアクセサリーの<c>InstanceId</c>を渡す
    /// </remarks>
    public sealed class EquippedAccessoryPopupController : PopupBase
    {
        [SerializeField]
        private AccessoryUIController accessoryUIPrefab;

        [SerializeField]
        private Transform listParent;

        [SerializeField]
        private Button unequipButton;

        [SerializeField]
        private Button closeButton;

        public EquippedAccessoryPopupController Setup(Player player)
        {
            var u = UserData.Instance;
            u.Accessories.List
                .Where(a => !u.IsEquipedAccessory(a) && a.AccessoryRecord.Rank <= player.CharacterRecord.Rank)
                .ForEach(a =>
            {
                Instantiate(this.accessoryUIPrefab, this.listParent, false)
                    .Setup(a.AccessoryRecord)
                    .Button
                    .OnClickAsObservable()
                    .SubscribeWithState2(this, a, (_, _this, _a) => _this.submit.OnNext(_a.InstanceId))
                    .AddTo(this);
            });

            this.unequipButton.OnClickAsObservable()
                .SubscribeWithState(this, (_, _this) => _this.submit.OnNext(0))
                .AddTo(this);

            this.closeButton.OnClickAsObservable()
                .SubscribeWithState(this, (_, _this) => _this.submit.OnNext(-1))
                .AddTo(this);

            return this;
        }
    }
}
