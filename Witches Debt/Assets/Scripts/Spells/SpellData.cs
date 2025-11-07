public struct SpellData
{
    public float baseDamage;
    public float size;
    public float speed;

    public SpellData(float baseDamage, float size, float speed)
    {
        this.baseDamage = baseDamage;
        this.size = size;
        this.speed = speed;
    }

    public SpellData(SpellData spellData)
    {
        this.baseDamage = spellData.baseDamage;
        this.size = spellData.size;
        this.speed = spellData.speed;
    }
}