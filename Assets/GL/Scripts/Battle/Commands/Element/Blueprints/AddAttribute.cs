using System;
using UnityEngine;

namespace GL.Battle.Commands.Element.Blueprints
{
    /// <summary>
    /// 属性値上昇コマンドの設定データ.
    /// </summary>
    [CreateAssetMenu(menuName = "GL/Commands/Blueprints/AddAttribute")]
    public sealed class AddAttribute : Blueprint
    {
        [SerializeField]
        private Implements.AddAttribute.Parameter parameter = new Implements.AddAttribute.Parameter();

        public override IImplement Create()
        {
            return new Implements.AddAttribute(this.parameter);
        }

#if UNITY_EDITOR
        public override string FileName => $"AddAttribute_{Enum.GetName(typeof(Constants.StatusParameterType), this.parameter.AttributeType)}_{this.parameter.Rate}";

        public override Blueprint SetupFromEditor(string data)
        {
            var s = data.Split('_');
            this.parameter = new Implements.AddAttribute.Parameter();
            this.parameter.AttributeType = (Constants.AttributeType)Enum.Parse(typeof(Constants.AttributeType), s[1]);
            this.parameter.Rate = float.Parse(s[2]);

            return this;
        }
#endif
    }
}
