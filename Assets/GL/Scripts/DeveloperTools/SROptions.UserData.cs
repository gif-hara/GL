using System.ComponentModel;
using GL.User;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// <see cref="UserData"/>用のデバッグ
/// </summary>
public partial class SROptions
{
    [Category(Category.UserData)]
    [Sort(100)]
    public int Gold => UserData.Instance.Wallet.Gold;

    [Category(Category.UserData)]
    [Sort(200)]
    public void AddGold()
    {
        const int value = 1000000;
        UserData.Instance.Wallet.AddFromGold(value);
        UserData.Instance.Save();
        Debug.Log($"Added Gold {value}");
    }
}
