using GL.Database;
using GL.User;
using HK.GL.Extensions;
using UnityEngine;

namespace GL.Home.UI
{
    /// <summary>
    /// バトルのパネルを制御するクラス
    /// </summary>
    public sealed class BattlePanelController : MonoBehaviour
    {
        [SerializeField]
        private BattlePartySelectButtonController controllerPrefab;

        [SerializeField]
        private RectTransform scrollParent;

        void Start()
        {
            foreach (var id in UserData.Instance.UnlockElements.EnemyParties)
            {
                var blueprint = MasterData.EnemyParty.GetById(id);
                var controller = Instantiate(this.controllerPrefab, this.scrollParent, false);
                controller.Initialize(blueprint);
            }
        }
    }
}
