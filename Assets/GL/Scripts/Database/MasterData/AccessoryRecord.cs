using GL.Battle.CharacterControllers;
using GL.Database;
using HK.Framework.Text;
using HK.GL.Extensions;
using UnityEngine;

namespace GL.Database
{
    /// <summary>
    /// キャラクターが装備できるアクセサリー
    /// </summary>
    [CreateAssetMenu(menuName = "GL/MasterData/Record/Accessory")]
    public sealed class AccessoryRecord : ScriptableObject, IMasterDataRecord
    {
        public string Id => this.name;

        [SerializeField]
        private StringAsset.Finder accessoryName;
        public string AccessoryName => this.accessoryName.Get;

        [SerializeField]
        private int rank;
        public int Rank => this.rank;

        [SerializeField]
        private SkillElement[] skillElements = new SkillElement[0];
        public SkillElement[] SkillElements => this.skillElements;

        [SerializeField]
        private ConditionalCommandRecord[] commands = new ConditionalCommandRecord[0];
        public ConditionalCommandRecord[] Commands => this.commands;
    }
}
