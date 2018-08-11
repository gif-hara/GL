using GL.Battle.Systems;
using UnityEngine.Assertions;

namespace GL.Battle.CharacterControllers.StatusAilments
{
    /// <summary>
    /// <see cref="Element"/>を生成するクラス
    /// </summary>
    public sealed class Factory
    {
        public static Element Create(int remainingTurn, Constants.StatusAilmentType type, CharacterAilmentController controller)
        {
            switch (type)
            {
                case Constants.StatusAilmentType.Demonization:
                    return new Element(remainingTurn, type, controller);
                case Constants.StatusAilmentType.Hardening:
                    return new Element(remainingTurn, type, controller);
                case Constants.StatusAilmentType.Sickle:
                    return new Element(remainingTurn, type, controller);
                case Constants.StatusAilmentType.PreEmpt:
                    return new Element(remainingTurn, type, controller);
                case Constants.StatusAilmentType.Regeneration:
                    return new Regeneration(remainingTurn, type, controller);
                case Constants.StatusAilmentType.Soldier:
                    return new Soldier(remainingTurn, type, controller);
                case Constants.StatusAilmentType.Chase:
                    return new Chase(remainingTurn, type, controller);
                case Constants.StatusAilmentType.Rage:
                    return new Rage(remainingTurn, type, controller);
                case Constants.StatusAilmentType.Protect:
                    return new Element(remainingTurn, type, controller);
                case Constants.StatusAilmentType.Poison:
                    return new Poison(remainingTurn, type, controller);
                case Constants.StatusAilmentType.Paralysis:
                    return new Paralysis(remainingTurn, type, controller);
                case Constants.StatusAilmentType.Sleep:
                    return new Sleep(remainingTurn, type, controller);
                case Constants.StatusAilmentType.Confuse:
                    return new Element(remainingTurn, type, controller);
                case Constants.StatusAilmentType.Berserk:
                    return new Element(remainingTurn, type, controller);
                case Constants.StatusAilmentType.Vitals:
                    return new Vitals(remainingTurn, type, controller);
                default:
                    Assert.IsTrue(false, string.Format("未対応の値です {0}", type));
                    return new Element(remainingTurn, type, controller);
            }
        }
    }
}
