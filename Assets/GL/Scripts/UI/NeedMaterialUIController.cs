using GL.Database;
using GL.User;
using HK.Framework.Text;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace GL.UI
{
    /// <summary>
    /// 必要素材UIを制御するクラス
    /// </summary>
    public sealed class NeedMaterialUIController : MonoBehaviour
    {
        [SerializeField]
        private Text materialName;

        [SerializeField]
        private Text amount;

        [SerializeField]
        private StringAsset.Finder amountFormat;

        public void Setup(NeedMaterial needMaterial)
        {
            this.materialName.text = needMaterial.Material.MaterialName;
            var possessionMaterial = UserData.Instance.Materials.Find(m => m.Id == needMaterial.Material.Id);
            var possessionNumber = possessionMaterial == null ? 0 : possessionMaterial.Count;
            this.amount.text = this.amountFormat.Format(possessionNumber.ToString(), needMaterial.Amount.ToString());
        }
    }
}
