using UnityEngine;
using System.Collections.Generic;
using GL.Scripts.Battle.CharacterControllers;
using GL.Scripts.Battle.Systems;
using GL.Scripts.Events.Battle;
using HK.Framework.EventSystems;
using UniRx;

namespace HK.GL.UI.Battle
{
    /// <summary>
    /// コマンドパネルを制御するクラス
    /// </summary>
    public sealed class SelectCommandPanelController : MonoBehaviour
    {
        [SerializeField]
        private SelectCommandButtonController buttonPrefab;

        void Awake()
        {
            Broker.Global.Receive<StartSelectCommand>()
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
                var button = Instantiate(this.buttonPrefab, this.transform);
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
            for (var i = 0; i < this.transform.childCount; i++)
            {
                Destroy(this.transform.GetChild(i).gameObject);
            }
        }
    }
}
