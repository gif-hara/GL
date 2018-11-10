using System;
using HK.Framework.Text;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Database
{
    /// <summary>
    /// 定数文字列
    /// </summary>
    [CreateAssetMenu(menuName = "GL/MasterData/ConstantString")]
    public sealed class ConstantString : ScriptableObject
    {
        [Serializable]
        public class EquipmentString
        {
            [SerializeField]
            private StringAsset.Finder dagger;

            [SerializeField]
            private StringAsset.Finder longSword;

            [SerializeField]
            private StringAsset.Finder shield;

            [SerializeField]
            private StringAsset.Finder katana;

            [SerializeField]
            private StringAsset.Finder staff;

            [SerializeField]
            private StringAsset.Finder rod;

            [SerializeField]
            private StringAsset.Finder bow;

            [SerializeField]
            private StringAsset.Finder knuckle;

            [SerializeField]
            private StringAsset.Finder spear;

            [SerializeField]
            private StringAsset.Finder hammer;

            [SerializeField]
            private StringAsset.Finder bangle;

            [SerializeField]
            private StringAsset.Finder arrow;

            [SerializeField]
            private StringAsset.Finder accessory;

            public string Get(Constants.EquipmentType type)
            {
                switch (type)
                {
                    case Constants.EquipmentType.Dagger:
                        return this.dagger.Get;
                    case Constants.EquipmentType.LongSword:
                        return this.longSword.Get;
                    case Constants.EquipmentType.Shield:
                        return this.shield.Get;
                    case Constants.EquipmentType.Katana:
                        return this.katana.Get;
                    case Constants.EquipmentType.Staff:
                        return this.staff.Get;
                    case Constants.EquipmentType.Rod:
                        return this.rod.Get;
                    case Constants.EquipmentType.Bow:
                        return this.bow.Get;
                    case Constants.EquipmentType.Knuckle:
                        return this.knuckle.Get;
                    case Constants.EquipmentType.Spear:
                        return this.spear.Get;
                    case Constants.EquipmentType.Hammer:
                        return this.hammer.Get;
                    case Constants.EquipmentType.Bangle:
                        return this.bangle.Get;
                    case Constants.EquipmentType.Arrow:
                        return this.arrow.Get;
                    case Constants.EquipmentType.Accessory:
                        return this.accessory.Get;
                    default:
                        Assert.IsTrue(false, $"{type}は未対応の値です");
                        return null;
                }
            }
        }

        [SerializeField]
        private EquipmentString equipment;
        public EquipmentString Equipment => this.equipment;
    }
}
