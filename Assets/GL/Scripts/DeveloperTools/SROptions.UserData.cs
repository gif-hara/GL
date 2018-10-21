using System.ComponentModel;
using GL.MasterData;
using GL.User;
using HK.GL.Extensions;
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
        UserData.Instance.Wallet.Gold.Add(value);
        UserData.Instance.Save();
        Debug.Log($"Added Gold {value}");
    }

    [Category(Category.UserData)]
    [Sort(200)]
    public void ZeroGold()
    {
        var u = UserData.Instance;
        u.Wallet.Gold.Add(-u.Wallet.Gold.Value);
        u.Save();
        Debug.Log($"Gold is Zero!");
    }

    [Category(Category.UserData)]
    [Sort(200)]
    public void AddExperience()
    {
        const int value = 1000000;
        UserData.Instance.Wallet.Experience.Add(value);
        UserData.Instance.Save();
        Debug.Log($"Added Experience {value}");
    }

    [Category(Category.UserData)]
    [Sort(200)]
    public void ZeroExperience()
    {
        var u = UserData.Instance;
        u.Wallet.Experience.Add(-u.Wallet.Experience.Value);
        u.Save();
        Debug.Log($"Experience is Zero!");
    }

    [Category(Category.UserData)]
    [Sort(200)]
    public void PrintAllWeapon()
    {
        UserData.Instance.Weapons.List.ForEach(w => Debug.Log($"[{w.InstanceId}] {w.BattleWeapon.WeaponName}"));
    }
    [Category(Category.UserData)]
    [Sort(200)]
    public void PrintAllMaterial()
    {
        UserData.Instance.Materials.ForEach(m => Debug.Log($"[{Database.Material.List.Find(d => d.Id == m.Id).MaterialName}] *{m.Count}"));
    }
}
