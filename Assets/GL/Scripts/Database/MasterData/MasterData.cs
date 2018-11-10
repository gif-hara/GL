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
        private EquipmentList equipment;
        public static EquipmentList Equipment => instance.equipment;

        [SerializeField]
        private MaterialList material;
        public static MaterialList Material => instance.material;

        [SerializeField]
        private EnemyPartyList enemyParty;
        public static EnemyPartyList EnemyParty => instance.enemyParty;

        [SerializeField]
        private ConstantCommand constantCommand;
        public static ConstantCommand ConstantCommand => instance.constantCommand;

        [SerializeField]
        private ConstantString constantString;
        public static ConstantString ConstantString => instance.constantString;

        public void Setup()
        {
            Assert.IsNull(instance);
            instance = this;
        }
    }
}
