using GL.Battle;
using GL.Battle.CharacterControllers.JobSystems;
using HK.Framework.Text;
using UnityEngine;

namespace GL.Battle.CharacterControllers
{
    /// <summary>
    /// キャラクターを構成する設計図
    /// </summary>
    [CreateAssetMenu(menuName = "GL/CharacterControllers/Blueprint")]
    public sealed class Blueprint : ScriptableObject
    {
        public string Id { get { return this.name; } }
        
        [SerializeField]
        private StringAsset.Finder characterName;
        public string CharacterName { get { return characterName.Get; } }
        
        [SerializeField]
        private Job job;
        public Job Job { get { return job; } }
        
        [SerializeField]
        private GameObject model;
        public GameObject Model { get { return this.model; } }

        public Parameter GetParameter(int level)
        {
            return this.job.EvaluteParameter(level);
        }

        public Resistance Resistance { get { return this.job.Resistance; } }
    }
}
