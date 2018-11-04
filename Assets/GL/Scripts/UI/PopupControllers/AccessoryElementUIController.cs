using GL.Database;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace GL.UI
{
    /// <summary>
    /// <see cref="SkillElement"/>をUIに設定するクラス
    /// </summary>
    public sealed class AccessoryElementUIController : MonoBehaviour
    {
        [SerializeField]
        private Text elementName;

        [SerializeField]
        private Text description;

        public void Setup(SkillElement element)
        {
            this.elementName.text = element.ElementName;
            this.description.text = element.Description;
        }
    }
}
