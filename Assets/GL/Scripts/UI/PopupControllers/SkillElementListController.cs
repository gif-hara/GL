using System.Collections.Generic;
using System.Linq;
using GL.Database;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.UI
{
    /// <summary>
    /// スキルリストを制御するクラス
    /// </summary>
    public sealed class SkillElementListController : MonoBehaviour
    {
        [SerializeField]
        private Transform parent;

        [SerializeField]
        private SkillElementUIController skillPrefab;

        [SerializeField]
        private GameObject noneObject;

        private readonly List<SkillElementUIController> skills = new List<SkillElementUIController>();

        public void Setup(IEnumerable<ConditionalSkillElement> skills)
        {
            this.Clear();
            this.noneObject.SetActive(!skills.Any());
            foreach (var s in skills)
            {
                var skill = Instantiate(this.skillPrefab, this.parent, false);
                skill.Setup(s);
                this.skills.Add(skill);
            }
        }

        public void Clear()
        {
            for (var i = 0; i < this.skills.Count; i++)
            {
                Destroy(this.skills[i].gameObject);
            }
            this.skills.Clear();
        }
    }
}
