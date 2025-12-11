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
        var data = new PlayerSaveData(MaxHP,
                                      MoveSpeed, 
                                      CastSpeed, 
                                      VampChance, 
                                      Regeneration, 
                                      HealPercent, 
                                      DodgeChance, 
                                      Armor,
                                      Greed);
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