using GL.Scripts.Battle.CharacterControllers;
using GL.Scripts.Battle.Systems;

namespace GL.Scripts.Battle.Commands.Implements
{
    /// <summary>
    /// 実際にゲームで使用するコマンドの抽象クラス
    /// </summary>
    public abstract class Implement<T> : IImplement where T : CommandParameter
    {
        protected T parameter;
        
        public string Name { get { return this.parameter.Name.Get; } }

        public Constants.TargetPartyType TargetPartyType { get { return this.parameter.TargetPartyType; } }
        
        public Constants.TargetType TargetType { get { return this.parameter.TargetType; } }

        public Constants.CommandType CommandType { get { return this.parameter.CommandType; } }

        protected Implement(T parameter)
        {
            this.parameter = parameter;
        }
        
        public abstract void Invoke(Character invoker);
    }
}
