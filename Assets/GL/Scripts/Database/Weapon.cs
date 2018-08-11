using System.Linq;
using UnityEditor;
using UnityEngine;

namespace GL.Scripts.Database
{
    /// <summary>
    /// 武器データベース
    /// </summary>
    [CreateAssetMenu(menuName = "GL/Database/Weapon")]
    public class Weapon : ScriptableObject
    {
        [SerializeField]
        private Battle.Weapon[] list;

        void Reset()
        {
            this.list = AssetDatabase.FindAssets("t:Weapon", new[] {"Assets/GL/MasterData/Weapons/Player"})
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<Battle.Weapon>)
                .ToArray();
        }
    }
}
