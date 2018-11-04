using GL.Database;
using GL.Extensions;
using HK.Framework.Text;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace GL.UI
{
    /// <summary>
    /// 素材リストの要素を制御するクラス
    /// </summary>
    public sealed class MaterialElement : MonoBehaviour
    {
        [SerializeField]
        private Text materialName;

        [SerializeField]
        private Text value;

        [SerializeField]
        private StringAsset.Finder valueFormat;

        public MaterialElement Setup(MaterialRecord material, int value)
        {
            this.materialName.text = material.MaterialName;
            this.value.text = this.valueFormat.Format(value.ToString()).RemoveLastNewLine();

            return this;
        }
    }
}
