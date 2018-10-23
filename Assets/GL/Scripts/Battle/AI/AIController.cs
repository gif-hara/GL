using System;
using GL.Battle.CharacterControllers;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Battle.AI
{
    /// <summary>
    /// AIを制御するクラス
    /// </summary>
    [CreateAssetMenu(menuName = "GL/AI/AIController")]
    public sealed class AIController : ScriptableObject
    {
        [SerializeField]
        private Bundle[] bundles;

        public void Invoke(Character character)
        {
            foreach(var b in this.bundles)
            {
                if(b.Condition.Suitable)
                {
                    b.Element.Invoke(character);
                    return;
                }
            }

            Assert.IsTrue(false, $"{character.StatusController.Name}が実行出来るコマンドがありませんでした");
        }

        [Serializable]
        public class Bundle
        {
            [SerializeField]
            private AICondition condition;
            public AICondition Condition => this.condition;

            [SerializeField]
            private AIElement element;
            public AIElement Element => this.element;
        }
    }
}
