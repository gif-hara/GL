using System.Collections.Generic;
using System.Linq;
using GL.Scripts.Battle.Commands.Impletents;
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
        private List<Commands.Blueprints.Blueprint> commandSettings;
        public List<IImplement> Commands { get { return this.commandSettings.Select(c => c.Create()).ToList(); } }

        /// <summary>
        /// 設定を元に<see cref="CharacterStatusController"/>を作成する
        /// </summary>
        public CharacterStatusController Create()
        {
            return new CharacterStatusController(this);
        }
    }
}
