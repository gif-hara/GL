using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Database
{
    /// <summary>
    /// データベース
    /// </summary>
    [CreateAssetMenu(menuName = "GL/MasterData/Database")]
    public class MasterData : ScriptableObject
    {
        private static MasterData instance;

        [SerializeField]
        private CharacterList character;
        public static CharacterList Character => instance.character;

        [SerializeField]
        private EquipmentList weapon;
        public static EquipmentList Weapon => instance.weapon;

        [SerializeField]
        private AccessoryList accessory;
        public static AccessoryList Accessory => instance.accessory;

        [SerializeField]
        private MaterialList material;
        public static MaterialList Material => instance.material;

        [SerializeField]
        private EnemyPartyList enemyParty;
        public static EnemyPartyList EnemyParty => instance.enemyParty;

        [SerializeField]
        private ConstantCommand constantCommand;
        public static ConstantCommand ConstantCommand => instance.constantCommand;

        public void Setup()
        {
            Assert.IsNull(instance);
            instance = this;
        }
    }
}
