using UnityEngine;
using UnityEngine.Assertions;

namespace GL.MasterData
{
    /// <summary>
    /// データベース
    /// </summary>
    [CreateAssetMenu(menuName = "GL/MasterData/Database")]
    public class Database : ScriptableObject
    {
        private static Database instance;

        [SerializeField]
        private Character character;
        public static Character Character => instance.character;

        [SerializeField]
        private Weapon weapon;
        public static Weapon Weapon => instance.weapon;

        [SerializeField]
        private Accessory accessory;
        public static Accessory Accessory => instance.accessory;

        [SerializeField]
        private Material material;
        public static Material Material => instance.material;

        [SerializeField]
        private EnemyParty enemyParty;
        public static EnemyParty EnemyParty => instance.enemyParty;

        public void Setup()
        {
            Assert.IsNull(instance);
            instance = this;
        }
    }
}
