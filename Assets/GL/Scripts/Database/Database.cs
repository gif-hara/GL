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
        private Weapon weapon;
        public static Weapon Weapon { get { return instance.weapon; } }

        public void Setup()
        {
            Assert.IsNull(instance);
            instance = this;
        }
    }
}
