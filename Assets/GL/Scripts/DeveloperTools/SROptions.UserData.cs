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
    [Sort(200)]
    public void AddGold()
    {
        const int value = 1000000;
        UserData.Instance.Wallet.AddFromGold(value);
        UserData.Instance.Save();
        Debug.Log($"Added Gold {value}");
    }

    [Category(Category.UserData)]
    [Sort(200)]
    public void ZeroGold()
    {
        var u = UserData.Instance;
        u.Wallet.AddFromGold(-u.Wallet.Gold);
        u.Save();
        Debug.Log($"Gold is Zero!");
    }

    [Category(Category.UserData)]
    [Sort(200)]
    public void PrintAllWeapon()
    {
        UserData.Instance.Weapons.List.ForEach(w => Debug.Log($"[{w.InstanceId}] {w.BattleWeapon.WeaponName}"));
    }
}
