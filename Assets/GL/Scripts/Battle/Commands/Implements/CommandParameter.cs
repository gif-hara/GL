using System;
using GL.Scripts.Battle.Systems;
using HK.Framework.Text;

namespace GL.Scripts.Battle.Commands.Implements
{
    [Serializable]
    public abstract class CommandParameter
    {
        public StringAsset.Finder Name;

        public Constants.TargetPartyType TargetPartyType;

        public Constants.TargetType TargetType;

        public Constants.PostprocessCommand Postprocess;
    }
}
