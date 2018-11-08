using GL.Database;
using GL.UI;
using GL.UI.PopupControllers;
using HK.Framework.Text;
using HK.GL.Extensions;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace GL.Home.UI
{
    /// <summary>
    /// ショップのアイテム購入確認ポップアップを制御するクラス
    /// </summary>
    public sealed class ConfirmShopPopupController : PopupBase
    {
        [SerializeField]
        private Button decide;

        [SerializeField]
        private Button cancel;

        [SerializeField]
        private Transform rankParent;

        [SerializeField]
        private GameObject rankPrefab;

        [SerializeField]
        private Text equipmentName;

        [SerializeField]
        private Text price;

        [SerializeField]
        private StringAsset.Finder priceFormat;

        [SerializeField]
        private Transform skillParent;

        [SerializeField]
        private Transform commandParent;

        [SerializeField]
        private Transform needMaterialParent;

        [SerializeField]
        private SkillElementUIController skillElementPrefab;

        [SerializeField]
        private CommandUIController commandPrefab;

        [SerializeField]
        private NeedMaterialUIController needMaterialPrefab;

        [SerializeField]
        private GameObject skillNoneObject;

        [SerializeField]
        private GameObject commandNoneObject;

        void Start()
        {
            this.decide.OnClickAsObservable()
                .SubscribeWithState(this, (_, _this) => _this.submit.OnNext(1))
                .AddTo(this);
                
            this.cancel.OnClickAsObservable()
                .SubscribeWithState(this, (_, _this) => _this.submit.OnNext(0))
                .AddTo(this);
        }

        public ConfirmShopPopupController Setup(EquipmentRecord equipment)
        {
            for (var i = 0; i < equipment.Rank; i++)
            {
                Instantiate(this.rankPrefab, this.rankParent);
            }

            this.equipmentName.text = equipment.EquipmentName;
            this.price.text = this.priceFormat.Format(equipment.Price);

            this.skillNoneObject.SetActive(equipment.SkillElements.Length == 0);
            equipment.SkillElements.ForEach(s =>
            {
                Instantiate(this.skillElementPrefab, this.skillParent, false).Setup(s);
            });

            this.commandNoneObject.SetActive(equipment.Commands.Length == 0);
            equipment.Commands.ForEach(c =>
            {
                Instantiate(this.commandPrefab, this.commandParent, false).Setup(c);
            });

            equipment.NeedMaterials.ForEach(n =>
            {
                Instantiate(this.needMaterialPrefab, this.needMaterialParent, false).Setup(n);
            });

            return this;
        }
    }
}
