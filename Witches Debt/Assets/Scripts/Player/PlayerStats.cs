public class PlayerStats
{
    private float MaxHP;
    private float MoveSpeed;
    private float CastSpeed;
    private float VampChance;
    private float Regeneration;
    private float HealPercent;
    private float DodgeChance;
    private float Armor;
    private float Greed;

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
}