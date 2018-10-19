using GL.User;
using SRDebugger;
using SRDebugger.Services;
using SRF.Service;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.DeveloperTools
{
    /// <summary>
    /// SRDebuggerのSystemにGLのデータを表示するクラス
    /// </summary>
    public sealed class SystemInformationService
    {
        public void Setup()
        {
            var systemInformation = SRServiceManager.GetService<ISystemInformationService>();
            systemInformation.Add(InfoEntry.Create("Wallet.Gold", () => UserData.Instance.Wallet.Gold.Value), "GL.UserData");
        }
    }
}
