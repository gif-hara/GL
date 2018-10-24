using GL.Events.Battle;
using HK.Framework.EventSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Battle.UI
{
    /// <summary>
    /// バトルのパネルを制御するクラス
    /// </summary>
    public sealed class BattlePanelController : MonoBehaviour
    {
        [SerializeField]
        private Transform enemyParent;

        [SerializeField]
        private Transform playerParent;

        [SerializeField]
        private CharacterUIController characterUIPrefab;

        void Awake()
        {
            Broker.Global.Receive<CreatedParties>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    x.Parties.Player.Members.ForEach(m => Object.Instantiate(_this.characterUIPrefab, _this.playerParent).Setup(m));
                    x.Parties.Enemy.Members.ForEach(m => Object.Instantiate(_this.characterUIPrefab, _this.enemyParent).Setup(m));
                });
        }
    }
}
