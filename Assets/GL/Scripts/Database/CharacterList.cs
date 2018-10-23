using System.Linq;
using UnityEngine;

namespace GL.Database
{
    /// <summary>
    /// キャラクターデータベース
    /// </summary>
    [CreateAssetMenu(menuName = "GL/MasterData/Character")]
    public class CharacterList : MasterDataRecordList<Battle.CharacterControllers.Blueprint>
    {
        protected override string FindAssetsFilter => "l:GK.Battle.CharacterControllers.Blueprint";

        protected override string[] FindAssetsPaths => new[] { "Assets/GL/MasterData/Characters/Player" };
    }
}
