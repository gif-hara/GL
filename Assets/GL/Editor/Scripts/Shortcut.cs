using HK.Framework.Systems;
using UnityEditor;

namespace GL.Editor.Scripts
{
    public sealed class Shortcut
    {
        [MenuItem("GL/Clear SaveData #&1")]
        private static void ClearSaveData()
        {
            if (EditorUtility.DisplayDialog("セーブデータ削除", "本当に削除しますか？", "OK", "Cancel"))
            {
                SaveData.Clear();
            }
        }
    }
}
