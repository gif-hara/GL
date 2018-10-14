﻿using GL.Battle;
using UnityEngine;

namespace GL.Battle.CharacterControllers
{
    /// <summary>
    /// キャラクターが装備するアクセサリーを制御するクラス
    /// </summary>
    public sealed class CharacterAccessoryController
    {
        public Accessory[] Accessories { get; private set; }
        
        public CharacterAccessoryController(Accessory[] accessories)
        {
            this.Accessories = accessories;
        }

        public void OnStartBattle(Character equippedCharacter)
        {
            foreach (var accessory in this.Accessories)
            {
                accessory.OnStartBattle(equippedCharacter);
            }
        }
    }
}
