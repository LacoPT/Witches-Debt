using Mono.Cecil;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;
public class PlayerStats : IInstanceModel
{
    public float MaxHP { get; set; }
    public float MoveSpeed { get; set; }
    public float CastSpeed { get; set; }
    public float VampChance { get; set; }
    public float Regeneration { get; set; }
    public float HealPercent { get; set; }
    public float DodgeChance { get; set; }
    public float Armor { get; set; }
    public float Greed { get; set; }

    public PlayerSaveData ToSaveData()
    {
        var data = new PlayerSaveData();
        data.MaxHP = MaxHP;
        data.MoveSpeed = MoveSpeed;
        data.CastSpeed = CastSpeed;
        data.VampChance = VampChance;
        data.Regeneration = Regeneration;
        data.HealPercent = HealPercent;
        data.DodgeChance = DodgeChance;
        data.Armor = Armor;
        data.Greed = Greed;
        return data;
    }

    public void FromSaveData(PlayerSaveData data)
    {
        MaxHP = data.MaxHP;
        MoveSpeed = data.MoveSpeed;
        CastSpeed = data.CastSpeed;
        VampChance = data.VampChance;
        Regeneration = data.Regeneration;
        HealPercent = data.HealPercent;
        DodgeChance = data.DodgeChance;
        Armor = data.Armor;
        Greed = data.Greed;
    }

    public GameObject CreateInstance()
    {
        var player = Object.Instantiate(prefab);
        player.Initialize(this);
        return player.gameObject;
    }
}