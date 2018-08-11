using GL.Battle.Systems;

namespace GL.Battle.CharacterControllers.StatusAilments
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
