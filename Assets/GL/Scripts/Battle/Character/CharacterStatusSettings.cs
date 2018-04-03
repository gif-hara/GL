using System.Collections.Generic;
using System.Linq;
using GL.Scripts.Battle.Commands.Blueprints;
using GL.Scripts.Battle.Commands.Impletents;
using UnityEngine;

namespace GL.Scripts.Battle.CharacterControllers
{
    /// <summary>
    /// キャラクターステータスを設定するヤーツ.
    /// </summary>
    [CreateAssetMenu()]
    public sealed class CharacterStatusSettings : ScriptableObject
    {
        [SerializeField]
        private GameObject model;
        public GameObject Model{ get{ return this.model; } }

        [SerializeField]
        private string characterName;
        public string Name{ get{ return this.characterName; } }

        [SerializeField]
        private int hitPoint;
        public int HitPoint{ get{ return this.hitPoint; } }

        [SerializeField]
        private int strength;
        public int Strength{ get{ return this.strength; } }

        [SerializeField]
        private int defense;
        public int Defense{ get{ return this.defense; } }

        [SerializeField]
        private int sympathy;
        public int Sympathy{ get{ return this.sympathy; } }

        [SerializeField]
        private int nega;
        public int Nega{ get{ return this.nega; } }

        [SerializeField]
        private int speed;
        public int Speed{ get{ return this.speed; } }

        [SerializeField]
        private List<Blueprint> commandSettings;
        public List<IImplement> Commands{ get{ return this.commandSettings.Select(c => c.Create()).ToList(); } }

        /// <summary>
        /// 設定を元に<see cref="CharacterStatus"/>を作成する
        /// </summary>
        public CharacterStatus Create()
        {
            return new CharacterStatus(this);
        }
    }
}
