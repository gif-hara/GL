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
    /// 武器変更のポップアップを制御するクラス
    /// </summary>
    /// <remarks>
    /// <see cref="this.submit"/>は装備したい武器の<c>InstanceId</c>を渡す
    /// </remarks>
    public sealed class EquippedWeaponPopupController : PopupBase
    {
        [SerializeField]
        private WeaponController weaponPrefab;

        [SerializeField]
        private Transform listParent;

        [SerializeField]
        private Button unequipButton;

        [SerializeField]
        private Button closeButton;

        public EquippedWeaponPopupController Setup(Player player, Constants.HandType handType)
        {
            // TODO: プレイヤーが装備可能な武器種類を抽出する
            var u = UserData.Instance;
            u.Weapons.List
                .Where(w => !u.IsEquipedWeapon(w) && w.BattleWeapon.Rank <= player.Blueprint.Rank)
                .ForEach(w =>
            {
                Instantiate(this.weaponPrefab, this.listParent, false)
                    .Setup(w.BattleWeapon)
                    .Button
                    .OnClickAsObservable()
                    .SubscribeWithState2(this, w, (_, _this, _w) => _this.submit.OnNext(_w.InstanceId))
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
