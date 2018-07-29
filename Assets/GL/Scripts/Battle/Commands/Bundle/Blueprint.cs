using GL.Scripts.Battle.Systems;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Scripts.Battle.Commands.Bundle
{
    /// <summary>
    /// <see cref="Commands.Blueprints"/>をまとめている実際に実行されるコマンドの設計図
    /// </summary>
    [CreateAssetMenu(menuName = "GL/Commands/Bundle")]
    public sealed class Blueprint : ScriptableObject
    {
        [SerializeField]
        private Implement.Parameter parameter;

        public Implement Create()
        {
            return new Implement(this.parameter);
        }
    }
}
