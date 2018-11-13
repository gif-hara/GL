using System;
using UnityEngine;

namespace GL.Battle.Commands.Element.Blueprints
{
    /// <summary>
    /// コマンド実行者に対してパラメータ倍率上昇コマンドの設定データ.
    /// </summary>
    [CreateAssetMenu(menuName = "GL/Commands/Blueprints/AddStatusParameterRateInvoker")]
    public sealed class AddStatusParameterRateInvoker : Blueprint
    {
        [SerializeField]
        private Implements.AddStatusParameterRateInvoker.Parameter parameter = new Implements.AddStatusParameterRateInvoker.Parameter();

        public override IImplement Create()
        {
            return new Implements.AddStatusParameterRateInvoker(this.parameter);
        }

#if UNITY_EDITOR
        public override string FileName => $"AddStatusParameterRateInvoker_{Enum.GetName(typeof(Constants.StatusParameterType), this.parameter.ParameterType)}_{this.parameter.Rate}";

        public override Blueprint SetupFromEditor(string data)
        {
            var s = data.Split('_');
            this.parameter = new Implements.AddStatusParameterRateInvoker.Parameter();
            this.parameter.ParameterType = (Constants.StatusParameterType)Enum.Parse(typeof(Constants.StatusParameterType), s[1]);
            this.parameter.Rate = float.Parse(s[2]);

            return this;
        }
#endif
    }
}
