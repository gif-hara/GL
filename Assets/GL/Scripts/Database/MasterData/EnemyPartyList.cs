using System.Linq;
using UnityEngine;

namespace GL.Database
{
    /// <summary>
    /// 敵パーティデータベース
    /// </summary>
    [CreateAssetMenu(menuName = "GL/MasterData/List/EnemyParty")]
    public sealed class EnemyPartyList : MasterDataRecordList<PartyRecord>
    {
        protected override string FindAssetsFilter => "l:GL.PartyRecord";

        protected override string[] FindAssetsPaths => new[] { "Assets/GL/MasterData/Parties/Enemy" };
    }
}
