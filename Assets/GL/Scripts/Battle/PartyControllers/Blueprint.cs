using System;
using System.Collections.Generic;
using GL.Scripts.Battle.CharacterControllers;
using GL.Scripts.Battle.Systems;
using UnityEngine;

namespace GL.Scripts.Battle.PartyControllers
{
    /// <summary>
    /// パーティの設計図
    /// </summary>
    [CreateAssetMenu(menuName = "GL/PartyControllers/Blueprint")]
    public sealed class Blueprint : ScriptableObject
    {
        [SerializeField]
        private Constants.CharacterType characterType;
        
        [SerializeField]
        private Element[] elements;

        public Party Create(Character controllerPrefab, Transform parent, Vector3 interval, float scaleX)
        {
            var member = new List<Character>();
            for (int i = 0; i < this.elements.Length; i++)
            {
                var character = this.CreateCharacter(
                    controllerPrefab,
                    this.elements[i],
                    parent,
                    interval * i,
                    scaleX
                );
                member.Add(character);
            }

            return new Party(member);
        }
        
        private Character CreateCharacter(
            Character controllerPrefab,
            Element element,
            Transform parent,
            Vector3 position,
            float scaleX
        )
        {
            var result = Instantiate(controllerPrefab, parent);
            result.transform.localPosition = position;
            var model = Instantiate(element.Character.Model, result.transform);
            model.transform.localPosition = Vector3.zero;
            var scale = model.transform.localScale;
            scale.x = scaleX;
            model.transform.localScale = scale;
            result.Initialize(element.Character, element.Level, characterType);

            return result;
        }
        
        [Serializable]
        public class Element
        {
            [SerializeField][Range(1.0f, 100.0f)]
            public int Level;

            [SerializeField]
            public CharacterControllers.Blueprint Character;
        }
    }
}
