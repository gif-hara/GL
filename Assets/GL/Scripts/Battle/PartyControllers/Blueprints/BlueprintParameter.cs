using System;
using UnityEngine;

namespace GL.Scripts.Battle.PartyControllers.Blueprints
{
    [Serializable]
    public abstract class BlueprintParameter
    {
        [SerializeField][Range(1.0f, 100.0f)]
        public int Level;

        public abstract CharacterControllers.Blueprints.Blueprint Character { get; }
    }
}
