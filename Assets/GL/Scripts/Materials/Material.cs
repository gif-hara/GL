using HK.Framework.Text;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Materials
{
    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(menuName = "GL/Material")]
    public sealed class Material : ScriptableObject
    {
        public string Id => this.name;

        [SerializeField]
        private StringAsset.Finder materialName;
        public string MaterialName => this.materialName.Get;
    }
}
