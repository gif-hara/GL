using UnityEngine;

namespace GL.Scripts.Database
{
    /// <summary>
    /// データベース
    /// </summary>
    [CreateAssetMenu(menuName = "GL/Database/Database")]
    public class Database : ScriptableObject
    {
        public Database Instance { get; private set; }

        [SerializeField]
        private Weapon weapon;
        public Weapon Weapon { get { return weapon; } }
    }
}
