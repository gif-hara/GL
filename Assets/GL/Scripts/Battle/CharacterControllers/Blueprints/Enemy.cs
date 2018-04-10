using GL.Scripts.Battle.Accessories;
using UnityEngine;

namespace GL.Scripts.Battle.CharacterControllers.Blueprints
{
    /// <summary>
    /// 敵の設計図
    /// </summary>
    [CreateAssetMenu(menuName = "GL/CharacterControllers/Blueprints/Enemy")]
    public sealed class Enemy : Blueprint
    {
        public override Accessory[] Accessories { get; set; }

        public override Commands.Blueprints.Blueprint[] Commands { get; set; }
    }
}
