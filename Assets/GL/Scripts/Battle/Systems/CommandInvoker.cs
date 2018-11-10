using GL.Battle.CharacterControllers;
using GL.Events.Battle;
using GL.Events.Home;
using GL.Extensions;
using HK.Framework.EventSystems;
using UniRx;
using UnityEngine;

namespace GL.Battle
{
    /// <summary>
    /// 行動するキャラクターのコマンドを実行する
    /// </summary>
    public sealed class CommandInvoker
    {
        private Character commandSelectCharacter;

        public CommandInvoker(GameObject owner)
        {
            Broker.Global.Receive<StartSelectCommand>()
                .SubscribeWithState(this, (x, _this) => _this.commandSelectCharacter = x.Character)
                .AddTo(owner);

            Broker.Global.Receive<SelectedCommand>()
                .Subscribe(OnSelectedCommand)
                .AddTo(owner);

            Broker.Global.Receive<SelectedTargets>()
                .Subscribe(OnSelectedTargets)
                .AddTo(owner);

            Broker.Global.Receive<ClickedFooterCancelButton>()
                .SubscribeWithState(this, (_, _this) => _this.OnClickedCancelButton())
                .AddTo(owner);
        }

        private static void OnSelectedCommand(SelectedCommand eventData)
        {
            var command = eventData.Command;
            var invoker = eventData.Invoker;
            var targets = command.GetTargets(invoker);

            // ターゲットを選択する必要がある場合はStartSelectTargetイベントを飛ばす
            if(command.TargetType.IsSelectType())
            {
                Broker.Global.Publish(StartSelectTarget.Get(invoker, command, targets));
            }
            // それ以外はコマンド実行
            else
            {
                Broker.Global.Publish(SelectedTargets.Get(invoker, command, targets));
            }
        }

        private static void OnSelectedTargets(SelectedTargets eventData)
        {
            eventData.Command.Invoke(eventData.Invoker, eventData.Targets);
        }

        private void OnClickedCancelButton()
        {
            Broker.Global.Publish(StartSelectCommand.Get(this.commandSelectCharacter));
        }
    }
}
