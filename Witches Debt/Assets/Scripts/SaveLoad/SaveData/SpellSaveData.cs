public class SpellSaveData
{
    public float BaseDamage { get; }
    public float Size { get; }
    public float Speed { get; }

    public SpellSaveData(float baseDamage, 
                         float size, 
                         float speed)
    {
        BaseDamage = baseDamage;
        Size = size;
        Speed = speed;
    }
}