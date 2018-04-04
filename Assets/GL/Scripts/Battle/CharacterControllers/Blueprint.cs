using UnityEngine;

namespace GL.Scripts.Battle.CharacterControllers
{
    /// <summary>
    /// キャラクターを構成する設計図
    /// </summary>
    [CreateAssetMenu(menuName = "GL/CharacterControllers/Blueprint")]
    public sealed class Blueprint : ScriptableObject
    {
        [SerializeField]
        private GameObject model;
        public GameObject Model { get { return this.model; } }

        [SerializeField]
        private CharacterStatus status;
        public CharacterStatus Status { get { return status; } }

        [SerializeField]
        private Commands.Blueprints.Blueprint[] commands;
        public Commands.Blueprints.Blueprint[] Commands { get { return this.commands; } }
    }
}
