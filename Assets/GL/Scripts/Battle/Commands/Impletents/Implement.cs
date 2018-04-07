using System;
using GL.Scripts.Battle.CharacterControllers;
using GL.Scripts.Battle.Systems;

namespace GL.Scripts.Battle.Commands.Impletents
{
    /// <summary>
    /// 実際にゲームで使用するコマンドの抽象クラス
    /// </summary>
    public abstract class Implement<T> : IImplement where T : Implement<T>.CommandParameter
    {
        public T Parameter { get; private set; }
        
        public string Name { get { return this.Parameter.Name; } }

        public Constants.TargetPartyType TargetPartyType { get { return this.Parameter.TargetPartyType; } }
        
        public Constants.TargetType TargetType { get { return this.Parameter.TargetType; } }

        protected Implement(T parameter)
        {
            this.Parameter = parameter;
        }
        
        public abstract void Invoke(Character invoker);

        [Serializable]
        public abstract class CommandParameter
        {
            public string Name;

            public Constants.TargetPartyType TargetPartyType;

            public Constants.TargetType TargetType;
        }
    }
}
