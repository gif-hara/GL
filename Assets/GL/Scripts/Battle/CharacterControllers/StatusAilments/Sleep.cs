using GL.Scripts.Battle.Systems;
using UnityEngine;

namespace GL.Scripts.Battle.CharacterControllers.StatusAilments
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Sleep : Element
    {
        public Sleep(int remainingTurn, Constants.StatusAilmentType type, CharacterAilmentController controller)
            : base(remainingTurn, type, controller)
        {
        }

        public override void TakeDamage()
        {
            base.TakeDamage();
            this.ForceRemove();
        }
    }
}
