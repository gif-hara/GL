using GL.Battle;

namespace GL.Battle.CharacterControllers.StatusAilments
{
    /// <summary>
    /// 状態異常の憤怒を制御するクラス
    /// </summary>
    public sealed class Rage : Element
    {
        public Rage(int remainingTurn, Constants.StatusAilmentType type, CharacterAilmentController controller)
            : base(remainingTurn, type, controller)
        {
        }

        public override void TakeDamage()
        {
            base.TakeDamage();
            this.controller.Character.StatusController.AddParameterToDynamic(
                Constants.StatusParameterType.Strength,
                Calculator.GetRageAmount(this.controller.Character.StatusController)
                );
        }
    }
}
