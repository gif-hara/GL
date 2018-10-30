using System.Linq;
using UnityEngine;

namespace GL.Database
{
    /// <summary>
    /// キャラクターデータベース
    /// </summary>
    [CreateAssetMenu(menuName = "GL/MasterData/List/Character")]
    public class CharacterList : MasterDataRecordList<CharacterRecord>
    {
        protected override string FindAssetsFilter => "l:GL.CharacterRecord";

        protected override string[] FindAssetsPaths => new[] { "Assets/GL/MasterData/Characters/Player" };
    }
}
