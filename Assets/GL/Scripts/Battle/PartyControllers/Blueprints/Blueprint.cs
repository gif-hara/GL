using System.Collections.Generic;
using GL.Scripts.Battle.CharacterControllers;
using GL.Scripts.Battle.Systems;
using HK.Framework.Text;
using UnityEngine;

namespace GL.Scripts.Battle.PartyControllers.Blueprints
{
    /// <summary>
    /// パーティの設計図
    /// </summary>
    public abstract class Blueprint : ScriptableObject
    {
        [SerializeField]
        private StringAsset.Finder partyName;

        public string PartyName { get { return partyName.Get; } }
        
        protected abstract Constants.CharacterType CharacterType { get; }

        protected abstract BlueprintParameter[] Parameters { get; }

        public Party Create(Character controllerPrefab, Transform parent, Vector3 interval, float scaleX)
        {
            var member = new List<Character>();
            for (int i = 0; i < this.Parameters.Length; i++)
            {
                var character = this.CreateCharacter(
                    i,
                    controllerPrefab,
                    parent,
                    interval * i,
                    scaleX
                );
                member.Add(character);
            }

            return new Party(member);
        }

        protected abstract Character CreateCharacter(
            int index,
            Character controllerPrefab,
            Transform parent,
            Vector3 position,
            float scaleX
        );

        protected Character InternalCreateCharacter(Character controllerPrefab, Transform parent, Vector3 position, GameObject modelPrefab, float scaleX)
        {
            var result = Instantiate(controllerPrefab, parent);
            result.transform.localPosition = position;
            var model = Instantiate(modelPrefab, result.transform);
            model.transform.localPosition = Vector3.zero;
            var scale = model.transform.localScale;
            scale.x = scaleX;
            model.transform.localScale = scale;

            return result;
        }
    }
}
