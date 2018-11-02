using System;
using HK.Framework.Text;
using HK.GL.Extensions;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Database
{
    /// <summary>
    /// 作成に必要な素材を持つクラス
    /// </summary>
    [Serializable]
    public sealed class NeedMaterial
    {
        [SerializeField]
        private MaterialRecord material;
        public MaterialRecord Material => this.material;

        [SerializeField]
        private int amount;
        public int Amount => this.amount;

        [SerializeField]
        private StringAsset.Finder materialName = null;

#if UNITY_EDITOR
        public void OnValidate()
        {
            if(this.materialName == null || !this.materialName.IsValid)
            {
                var stringAsset = AssetDatabase.LoadAssetAtPath<StringAsset>("Assets/GL/StringAssets/MaterialName.asset");
                this.materialName = new StringAsset.Finder(stringAsset, new StringAsset.Data());
            }
            else
            {
                var database = AssetDatabase.LoadAssetAtPath<MaterialList>("Assets/GL/MasterData/Database/Material.asset");
                this.material = database.List.Find(m => m.MaterialName == this.materialName.Get);
                Assert.IsNotNull(this.material, $"{this.materialName.Get}の素材がありませんでした");
            }
        }
#endif
    }
}
