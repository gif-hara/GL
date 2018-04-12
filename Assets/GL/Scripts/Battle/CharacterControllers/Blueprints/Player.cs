using GL.Scripts.Battle.Accessories;
using GL.Scripts.Battle.Weapons;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Scripts.Battle.CharacterControllers.Blueprints
{
    /// <summary>
    /// プレイヤーの設計図
    /// </summary>
    [CreateAssetMenu(menuName = "GL/CharacterControllers/Blueprints/Player")]
    public sealed class Player : Blueprint
    {
        [SerializeField]
        private Weapon weapon;
        public Weapon Weapon { get { return weapon; } set { weapon = value; } }
        
        [SerializeField]
        private Accessory[] accessories;

        public override Commands.Blueprints.Blueprint[] Commands { get { return this.weapon.Commands; } set { Assert.IsTrue(false, "想定外の挙動です"); } }

        public override Accessory[] Accessories { get { return this.accessories; } set { this.accessories = value; } }
    }
}
