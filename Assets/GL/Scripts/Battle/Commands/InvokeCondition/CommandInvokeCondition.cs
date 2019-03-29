using System.Collections.Generic;
using GL.Battle.CharacterControllers;
using HK.Framework.Text;
using UnityEngine;

namespace GL.Battle
{
    /// <summary>
    /// コマンドを実行できるか判断する抽象クラス
    /// </summary>
    public abstract class CommandInvokeCondition : ScriptableObject
    {
        [SerializeField]
        protected StringAsset.Finder description = null;
        public virtual string Description => this.description.Get;

        /// <summary>
        /// 条件を満たしているキャラクターを返す
        /// </summary>
        public abstract Character[] Suitable(BattleManager battleManager, Character invoker);
    }
}
