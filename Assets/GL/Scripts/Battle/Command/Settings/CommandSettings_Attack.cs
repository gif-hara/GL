using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using System;
using HK.Framework;

namespace HK.GL.Battle
{
    /// <summary>
    /// 攻撃コマンドの設定データ.
    /// </summary>
    [CreateAssetMenu()]
    public sealed class CommandSettings_Attack : CommandSettings
    {
        [SerializeField]
        private float rate;
         
        public override ICommand Create()
        {
            return new Command_Attack(this.commandName.Get, this.targetType, this.rate);
        }
    }
}
