﻿using GL.Scripts.Battle.Command.Impletents;
using UnityEngine;

namespace GL.Scripts.Battle.Command.Blueprints
{
    /// <summary>
    /// 攻撃コマンドの設定データ.
    /// </summary>
    [CreateAssetMenu()]
    public sealed class Attack : Blueprint
    {
        [SerializeField]
        private float rate;
         
        public override IImplement Create()
        {
            return new Impletents.Attack(this.commandName.Get, this.targetType, this.rate);
        }
    }
}
