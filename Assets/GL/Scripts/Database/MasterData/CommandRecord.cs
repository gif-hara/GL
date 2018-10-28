using GL.User;
using UnityEngine;

namespace GL.Database
{
    /// <summary>
    /// <see cref="Commands.Blueprints"/>をまとめている実際に実行されるコマンドのレコード
    /// </summary>
    [CreateAssetMenu(menuName = "GL/Commands/Bundle")]
    public sealed class CommandRecord : ScriptableObject
    {
        public string Id => this.name;
        
        [SerializeField]
        private Battle.Commands.Bundle.Implement.Parameter parameter;
        public Battle.Commands.Bundle.Implement.Parameter Parameter => this.parameter;

        public bool CanCreate(Player player)
        {
            return false;
        }

        public Battle.Commands.Bundle.Implement Create()
        {
            return new Battle.Commands.Bundle.Implement(this.parameter);
        }
    }
}
