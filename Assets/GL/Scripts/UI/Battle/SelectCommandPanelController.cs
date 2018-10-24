using UnityEngine;
using System.Collections.Generic;
using GL.Battle.CharacterControllers;
using GL.Battle;
using GL.Events.Battle;
using HK.Framework.EventSystems;
using UniRx;
using GL;

namespace GL.Battle.UI
{
    /// <summary>
    /// コマンドパネルを制御するクラス
    /// </summary>
    public sealed class SelectCommandPanelController : MonoBehaviour
    {
        [SerializeField]
        private Transform buttonParent;

        [SerializeField]
        private SelectCommandButtonController buttonPrefab;

        void Awake()
        {
            Broker.Global.Receive<VisibleRequestSelectCommandPanel>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    if (x.Character.CharacterType == Constants.CharacterType.Player)
                    {
                        _this.OnSelectCommandFromPlayer(x.Character);
                    }
                    else
                    {
                        _this.OnSelectCommandFromEnemy();
                    }
                })
                .AddTo(this);

            Broker.Global.Receive<SelectedCommand>()
                .SubscribeWithState(this, (_, _this) => _this.DestroyButtons())
                .AddTo(this);
        }

        private void OnSelectCommandFromPlayer(Character character)
        {
            this.DestroyButtons();
            if (!character.CanMove)
            {
                return;
            }
            
            var commands = character.StatusController.Commands;
            for (int i = 0; i < commands.Length; i++)
            {
                var button = Instantiate(this.buttonPrefab, this.buttonParent);
                var command = commands[i];
                button.SetProperty(character, command);
            }
        }

        private void OnSelectCommandFromEnemy()
        {
            this.DestroyButtons();
        }

        private void DestroyButtons()
        {
            // TODO: ObjectPool使う？
            for (var i = 0; i < this.buttonParent.childCount; i++)
            {
                Destroy(this.buttonParent.GetChild(i).gameObject);
            }
        }
    }
}
