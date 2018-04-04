using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System.Text;
using GL.Scripts.Battle.CharacterControllers;
using GL.Scripts.Battle.Systems;
using HK.Framework.EventSystems;

namespace HK.GL.Test.Battle
{
    /// <summary>
    /// 全てのキャラクターのステータスを表示するヤーツ.
    /// </summary>
    public sealed class DrawAllCharacterStatus : MonoBehaviour
    {
        [SerializeField]
        [Multiline()]
        private string format;

        private Text cachedText;
        
        void Awake()
        {
            this.cachedText = this.GetComponent<Text>();
            this.Subscribe(Broker.Global.Receive<Events.Battle.NextTurn>());
        }

        private void Draw()
        {
            var builder = new StringBuilder();
            BuildText(builder, this.format, BattleManager.Instance.Parties.Player);
            builder.AppendLine("------------------------------");
            BuildText(builder, this.format, BattleManager.Instance.Parties.Enemy);
            this.cachedText.text = builder.ToString();
        }

        private void Subscribe<T>(IObservable<T> observable)
        {
            observable
                .Subscribe(_ => this.Draw())
                .AddTo(this);
        }

        private static void BuildText(StringBuilder builder, string format, Party party)
        {
            party.Members.ForEach(m =>
            {
                builder.AppendLine(string.Format(
                    format,
                    m.StatusController.Status.Name,
                    m.StatusController.HitPointMax,
                    m.StatusController.Status.HitPoint,
                    m.StatusController.Status.Strength,
                    m.StatusController.Status.Defense,
                    m.StatusController.Status.Sympathy,
                    m.StatusController.Status.Nega,
                    m.StatusController.Status.Speed,
                    m.StatusController.Wait
                    )
                    );
            });
        }
    }
}
