using GL.Battle.CharacterControllers;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Battle.AIControllers
{
    /// <summary>
    /// ターン数が余剰と一致しているか
    /// </summary>
    [CreateAssetMenu(menuName = "GL/AI/Condition/IsTurnMatchedSurplus")]
    public sealed class IsTurnMatchedSurplus : Condition
    {
        [SerializeField]
        private int divide;

        [SerializeField]
        private int matchNumber;

        public override bool Suitable(Character invoker)
        {
            return (invoker.AIController.InvokedCount % this.divide) == this.matchNumber;
        }
    }
}
