using UnityEngine;
using UnityEngine.Assertions;

namespace GL
{
    /// <summary>
    /// カラーアセット
    /// </summary>
    [CreateAssetMenu(menuName = "GL/ColorAsset")]
    public sealed class ColorAsset : ScriptableObject
    {
        [SerializeField]
        private Color[] colors;
        public Color[] Colors => this.colors;
    }
}
