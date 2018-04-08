using System;
using GL.Scripts.Battle.Systems;
using UnityEngine.Assertions;

namespace GL.Scripts.Battle.CharacterControllers.StatusAilments
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
                case Constants.StatusAilmentType.Poison:
                    return new Poison(remainingTurn, type, controller);
                case Constants.StatusAilmentType.Paralysis:
                    return new Element(remainingTurn, type, controller);
                case Constants.StatusAilmentType.Sleep:
                    return new Element(remainingTurn, type, controller);
                case Constants.StatusAilmentType.Confuse:
                    return new Element(remainingTurn, type, controller);
                case Constants.StatusAilmentType.Berserk:
                    return new Element(remainingTurn, type, controller);
                default:
                    Assert.IsTrue(false, string.Format("未対応の値です {0}", type));
                    return new Element(remainingTurn, type, controller);
            }
        }
    }
}
