using System;
using UnityEngine;

namespace GL.Battle.Commands.Element.Blueprints
{
    /// <summary>
    /// パラメータ倍率上昇コマンドの設定データ.
    /// </summary>
    [CreateAssetMenu(menuName = "GL/Commands/Blueprints/AddStatusParameterRate")]
    public sealed class AddStatusParameterRate : Blueprint
    {
        [SerializeField]
        private Implements.AddStatusParameterRate.Parameter parameter = new Implements.AddStatusParameterRate.Parameter();

        public override IImplement Create()
        {
            return new Implements.AddStatusParameterRate(this.parameter);
        }

#if UNITY_EDITOR
        public override string FileName => $"AddStatusParameterRate_{Enum.GetName(typeof(Constants.StatusParameterType), this.parameter.ParameterType)}_{this.parameter.Rate}";

        public override Blueprint SetupFromEditor(string data)
        {
            var s = data.Split('_');
            this.parameter = new Implements.AddStatusParameterRate.Parameter();
            this.parameter.ParameterType = (Constants.StatusParameterType)Enum.Parse(typeof(Constants.StatusParameterType), s[1]);
            this.parameter.Rate = float.Parse(s[2]);

            return this;
        }
#endif
    }
}
