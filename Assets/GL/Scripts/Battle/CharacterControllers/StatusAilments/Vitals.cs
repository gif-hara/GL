using GL.Scripts.Battle.Systems;

namespace GL.Scripts.Battle.CharacterControllers.StatusAilments
{
    /// <summary>
    /// 急所を制御するクラス
    /// </summary>
    public sealed class Vitals : Element
    {
        public Vitals(int remainingTurn, Constants.StatusAilmentType type, CharacterAilmentController controller)
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
