using System.Linq;
using UnityEngine;

namespace GL.MasterData
{
    /// <summary>
    /// キャラクターデータベース
    /// </summary>
    [CreateAssetMenu(menuName = "GL/MasterData/Character")]
    public class Character : DatabaseList<Battle.CharacterControllers.Blueprint>
    {
        protected override string FindAssetsFilter => "l:GK.Battle.CharacterControllers.Blueprint";

        protected override string[] FindAssetsPaths => new[] { "Assets/GL/MasterData/Characters/Player" };
    }
}
