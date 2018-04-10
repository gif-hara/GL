using GL.Scripts.Battle.Accessories;
using GL.Scripts.Battle.CharacterControllers.JobSystems;
using GL.Scripts.Battle.Weapons;
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
        
        [SerializeField][Range(1.0f, 100.0f)]
        private int level;
        
        [SerializeField]
        private Job job;
        
        [SerializeField]
        private GameObject model;
        public GameObject Model { get { return this.model; } }

        [SerializeField]
        private Weapon weapon;
        
        [SerializeField]
        private Accessory[] accessories;
        public Accessory[] Accessories { get { return accessories; } }
        
        public Commands.Blueprints.Blueprint[] Commands { get { return this.weapon.Commands; } }

        public Parameter Parameter { get { return this.job.EvaluteParameter(this.level); } }

        public Resistance Resistance { get { return this.job.Resistance; } }
    }
}
