using System;
using GL.Battle;
using HK.Framework.Text;
using UnityEngine;

namespace GL.Battle.CharacterControllers.JobSystems
{
    /// <summary>
    /// 職業
    /// </summary>
    [CreateAssetMenu(menuName = "GL/CharacterControllers/Job")]
    public sealed class Job : ScriptableObject
    {
        [SerializeField]
        private StringAsset.Finder jobName;

        [SerializeField][EnumFlags]
        private Constants.WeaponType equipable;
        
        public string JobName { get { return jobName.Get; } }
    }
}
