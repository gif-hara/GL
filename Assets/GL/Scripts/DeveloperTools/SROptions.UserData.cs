using System.ComponentModel;
using System.Linq;
using GL.Database;
using GL.User;
using HK.GL.Extensions;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// <see cref="UserData"/>用のデバッグ
/// </summary>
public partial class SROptions
{
    [Category(Category.UserData_Gold)]
    [Sort(100)]
    [DisplayName("Add")]
    public void AddGold()
    {
        const int value = 1000000;
        UserData.Instance.Wallet.Gold.Add(value);
        UserData.Instance.Save();
        Debug.Log($"Added Gold {value}");
    }

    [Category(Category.UserData_Gold)]
    [Sort(100)]
    [DisplayName("Zero")]
    public void ZeroGold()
    {
        var u = UserData.Instance;
        u.Wallet.Gold.Add(-u.Wallet.Gold.Value);
        u.Save();
        Debug.Log($"Gold is Zero!");
    }

    [Category(Category.UserData_Experience)]
    [Sort(100)]
    [DisplayName("Add")]
    public void AddExperience()
    {
        const int value = 1000000;
        UserData.Instance.Wallet.Experience.Add(value);
        UserData.Instance.Save();
        Debug.Log($"Added Experience {value}");
    }

    [Category(Category.UserData_Experience)]
    [Sort(100)]
    [DisplayName("Zero")]
    public void ZeroExperience()
    {
        var u = UserData.Instance;
        u.Wallet.Experience.Add(-u.Wallet.Experience.Value);
        u.Save();
        Debug.Log($"Experience is Zero!");
    }

    [Category(Category.UserData_Weapon)]
    [Sort(100)]
    [DisplayName("Print")]
    public void PrintAllWeapon()
    {
        UserData.Instance.Equipments.List.ForEach(w => Debug.Log($"[{w.InstanceId}] {w.EquipmentRecord.EquipmentName}"));
    }

    [Category(Category.UserData_Material)]
    [Sort(100)]
    [DisplayName("Print")]
    public void PrintAllMaterial()
    {
        UserData.Instance.Materials.ForEach(m => Debug.Log($"[{MasterData.Material.GetById(m.Id).MaterialName}] *{m.Count}"));
    }

    [Category(Category.UserData_UnlockElements_EnemyParty)]
    [Sort(100)]
    [DisplayName("Unlock All")]
    public void UnlockEnemyPartyAll()
    {
        var e = UserData.Instance.UnlockElements.EnemyParties;
        e.Clear();
        e.AddRange(MasterData.EnemyParty.List.Select(p => p.Id));
        UserData.Instance.Save();
    }

    [Category(Category.UserData_UnlockElements_EnemyParty)]
    [Sort(100)]
    [DisplayName("Remove")]
    public void RemoveUnlockEnemyParty()
    {
        var e = UserData.Instance.UnlockElements.EnemyParties;
        e.RemoveRange(1, e.Count - 1);
        UserData.Instance.Save();
    }

    [Category(Category.UserData_UnlockElements_Character)]
    [Sort(100)]
    [DisplayName("Unlock All")]
    public void UnlockCharacterAll()
    {
        var c = UserData.Instance.UnlockElements.Characters;
        c.Clear();
        c.AddRange(MasterData.Character.List.Select(x => x.Id));
        UserData.Instance.Save();
    }

    [Category(Category.UserData_UnlockElements_Character)]
    [Sort(100)]
    [DisplayName("Remove")]
    public void RemoveUnlockCharacter()
    {
        var c = UserData.Instance.UnlockElements.Characters;
        c.RemoveAll(x => true);
        UserData.Instance.Save();
    }

    [Category(Category.UserData_UnlockElements_Equipment)]
    [Sort(100)]
    [DisplayName("Unlock All")]
    public void UnlockEquipmentAll()
    {
        var e = UserData.Instance.UnlockElements.Equipments;
        e.Clear();
        e.AddRange(MasterData.Equipment.List.Select(x => x.Id));
        UserData.Instance.Save();
    }

    [Category(Category.UserData_UnlockElements_Equipment)]
    [Sort(100)]
    [DisplayName("Remove")]
    public void RemoveUnlockEquipment()
    {
        var w = UserData.Instance.UnlockElements.Equipments;
        w.RemoveAll(x => true);
        UserData.Instance.Save();
    }
}
