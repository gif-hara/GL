using GL.Database;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace GL.UI
{
    /// <summary>
    /// <see cref="SkillElement"/>をUIに設定するクラス
    /// </summary>
    public sealed class SkillElementUIController : MonoBehaviour
    {
        [SerializeField]
        private Text elementName;

        [SerializeField]
        private Text description;

        public void Setup(ConditionalSkillElement element)
        {
            this.elementName.text = element.SkillElement.ElementName;
            this.description.text = element.SkillElement.Description;
        }
    }
}
