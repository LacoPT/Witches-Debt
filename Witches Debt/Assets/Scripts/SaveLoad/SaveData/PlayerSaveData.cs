public class PlayerSaveData
{
    public float MaxHP { get; }
    public float MoveSpeed { get; }
    public float CastSpeed { get; }
    public float VampChance { get; }
    public float Regeneration { get; }
    public float HealPercent { get; }
    public float DodgeChance { get; }
    public float Armor { get; }
    public float Greed { get; }

    public PlayerSaveData(float maxHP, 
                          float moveSpeed, 
                          float castSpeed, 
                          float vampChance, 
                          float regeneration, 
                          float healPercent, 
                          float dodgeChance, 
                          float armor, 
                          float greed)
    {
        MaxHP = maxHP;
        MoveSpeed = moveSpeed;
        CastSpeed = castSpeed;
        VampChance = vampChance;
        Regeneration = regeneration;
        HealPercent = healPercent;
        DodgeChance = dodgeChance;
        Armor = armor;
        Greed = greed;
    }
}