using System.Collections.Generic;
using System.Linq;
using GL.Battle.CharacterControllers;

namespace GL.Battle
{
    /// <summary>
    /// コマンドを実行した際の結果
    /// </summary>
    public sealed class InvokedCommandResult
    {
        /// <summary>
        /// コマンドを実行したキャラクター
        /// </summary>
        public Character InvokedCharacter;

        public Battle.Commands.Bundle.Implement InvokedCommand;
        
        public readonly List<TakeDamage> TakeDamages = new List<TakeDamage>();
        
        public readonly List<AddAilment> AddAilments = new List<AddAilment>();
        
        public readonly List<AddParameter> AddParameters = new List<AddParameter>();

        public readonly List<AddAttribute> AddAttributes = new List<AddAttribute>();

        public void Reset()
        {
            this.InvokedCommand = null;
            this.TakeDamages.Clear();
            this.AddAilments.Clear();
            this.AddParameters.Clear();
        }

        public override string ToString()
        {
            return string.Format("InvokedCommand {1}{0}TakeDamages {2}{0}AddAilments {3}{0}AddParameters {4}",
                System.Environment.NewLine,
                this.InvokedCommand,
                string.Join(",", this.TakeDamages.Select(x => x.ToString()).ToArray()),
                string.Join(",", this.AddAilments.Select(x => x.ToString()).ToArray()),
                string.Join(",", this.AddParameters.Select(x => x.ToString()).ToArray())
            );
        }

        public class TakeDamage
        {
            public readonly Character Invoker;

            public readonly Character Target;

            public readonly int Damage;

            public readonly bool IsHit;

            public TakeDamage(Character invoker, Character target, int damage, bool isHit)
            {
                this.Invoker = invoker;
                this.Target = target;
                this.Damage = damage;
                this.IsHit = isHit;
            }

            public override string ToString()
            {
                return $"{this.Invoker.StatusController.Name} , {this.Target.StatusController.Name} , {this.Damage} , {this.IsHit}";
            }
        }

        public class AddAilment
        {
            public readonly Character Target;

            public readonly Constants.StatusAilmentType Type;

            public AddAilment(Character target, Constants.StatusAilmentType type)
            {
                this.Target = target;
                this.Type = type;
            }

            public override string ToString()
            {
                return string.Format("[{0} {1}]", this.Target.StatusController.Name, this.Type);
            }
        }

        public class AddParameter
        {
            public readonly Character Target;

            public readonly Constants.StatusParameterType Type;

            public readonly int Amount;

            public AddParameter(Character target, Constants.StatusParameterType type, int amount)
            {
                this.Target = target;
                this.Type = type;
                this.Amount = amount;
            }

            public override string ToString()
            {
                return string.Format("[{0} {1} {2}]", this.Target.StatusController.Name, this.Type, this.Amount);
            }
        }

        public class AddAttribute
        {
            public readonly Character Target;

            public readonly Constants.AttributeType Type;

            public readonly float Amount;

            public AddAttribute(Character target, Constants.AttributeType type, float amount)
            {
                this.Target = target;
                this.Type = type;
                this.Amount = amount;
            }

            public override string ToString()
            {
                return string.Format("[{0} {1} {2}]", this.Target.StatusController.Name, this.Type, this.Amount);
            }
        }
    }
}
