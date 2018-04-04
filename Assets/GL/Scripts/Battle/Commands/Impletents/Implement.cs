﻿using GL.Scripts.Battle.CharacterControllers;
using GL.Scripts.Battle.Systems;

namespace GL.Scripts.Battle.Commands.Impletents
{
    /// <summary>
    /// 実際にゲームで使用するコマンドの抽象クラス
    /// </summary>
    public abstract class Implement : IImplement
    {
        public string Name { get; private set; }

        public Constants.TargetType TargetType { get; private set; }

        protected Implement(string name, Constants.TargetType targetType)
        {
            this.Name = name;
            this.TargetType = targetType;
        }

        public abstract void Invoke(Character invoker);
    }
}
