using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System.Text;
using GL.Scripts.Battle.CharacterControllers;
using HK.Framework.EventSystems;
using HK.GL.Battle;

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
            BuildText(builder, this.format, BattleManager.Instance.Party.Player);
            builder.AppendLine("------------------------------");
            BuildText(builder, this.format, BattleManager.Instance.Party.Enemy);
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
                    m.Status.Name,
                    m.Status.HitPointMax,
                    m.Status.HitPoint,
                    m.Status.Strength,
                    m.Status.Defense,
                    m.Status.Sympathy,
                    m.Status.Nega,
                    m.Status.Speed,
                    m.Status.Wait
                    )
                    );
            });
        }
    }
}
