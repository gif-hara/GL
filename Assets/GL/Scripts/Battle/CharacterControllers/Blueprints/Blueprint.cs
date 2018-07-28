using GL.Scripts.Battle.Accessories;
using GL.Scripts.Battle.CharacterControllers.JobSystems;
using HK.Framework.Text;
using UnityEngine;

namespace GL.Scripts.Battle.CharacterControllers.Blueprints
{
    /// <summary>
    /// キャラクターを構成する設計図
    /// </summary>
    [CreateAssetMenu(menuName = "GL/CharacterControllers/Blueprint")]
    public class Blueprint : ScriptableObject
    {
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
