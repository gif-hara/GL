using GL.Scripts.Battle.PartyControllers.Blueprints;
using UnityEngine;

namespace GL.Scripts.Home.UI
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

        [SerializeField]
        private Enemy[] blueprints;

        void Start()
        {
            foreach (var blueprint in this.blueprints)
            {
                var controller = Instantiate(this.controllerPrefab, this.scrollParent, false);
                controller.Initialize(blueprint);
            }
        }
    }
}
