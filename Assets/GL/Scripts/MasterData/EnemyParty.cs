﻿using System.Linq;
using UnityEngine;

namespace GL.MasterData
{
    /// <summary>
    /// 敵パーティデータベース
    /// </summary>
    [CreateAssetMenu(menuName = "GL/MasterData/EnemyParty")]
    public sealed class EnemyParty : DatabaseList<Battle.PartyControllers.Blueprint>
    {
        protected override string FindAssetsFilter => "l:GK.Battle.PartyControllers.Blueprint";

        protected override string[] FindAssetsPaths => new[] { "Assets/GL/MasterData/Parties/Enemy" };
    }
}
