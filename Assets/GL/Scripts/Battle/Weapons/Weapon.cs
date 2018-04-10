using GL.Scripts.Battle.Commands.Blueprints;
using UnityEngine;

namespace GL.Scripts.Battle.Weapons
{
    /// <summary>
    /// 武器クラス
    /// </summary>
    /// <remarks>
    /// コマンドリストを担う
    /// </remarks>
    [CreateAssetMenu(menuName = "GL/Weapon")]
    public sealed class Weapon : ScriptableObject
    {
        [SerializeField]
        private Blueprint[] commands;
        public Blueprint[] Commands { get { return commands; } }
    }
}
