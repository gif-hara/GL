using GL.Database;
using HK.Framework.Text;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Database
{
    /// <summary>
    /// 素材レコード
    /// </summary>
    [CreateAssetMenu(menuName = "GL/MasterData/Record/Material")]
    public sealed class MaterialRecord : ScriptableObject, IMasterDataRecord
    {
        public string Id => this.name;

        [SerializeField]
        private StringAsset.Finder materialName;
        public string MaterialName => this.materialName.Get;
    }
}
