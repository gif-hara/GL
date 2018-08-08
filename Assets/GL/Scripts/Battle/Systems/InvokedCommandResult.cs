using System.Collections.Generic;
using System.Linq;
using GL.Scripts.Battle.CharacterControllers;
using GL.Scripts.Battle.Commands.Implements;

namespace GL.Scripts.Battle.Systems
{
    /// <summary>
    /// コマンドを実行した際の結果
    /// </summary>
    public sealed class InvokedCommandResult
    {
        public Battle.Commands.Bundle.Implement InvokedCommand;
        
        public readonly List<TakeDamage> TakeDamages = new List<TakeDamage>();
        
        public readonly List<AddAilment> AddAilments = new List<AddAilment>();
        
        public readonly List<AddParameter> AddParameters = new List<AddParameter>();

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
            public readonly Character Target;

            public readonly int Damage;

            public TakeDamage(Character target, int damage)
            {
                this.Target = target;
                this.Damage = damage;
            }

            public override string ToString()
            {
                return string.Format("[{0} {1}]", this.Target.StatusController.Name, this.Damage);
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
    }
}
