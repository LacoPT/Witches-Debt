public struct SpellData
{
    //THOSE ARE INTENDED TO BE CHANGED EXTERNALLY, BUT THIS IS BAD BECAUSE THIS IS HORRIBLE, FIX IT
    public float baseDamage;
    public float size;
    public float speed;
    public float lifeTime;

    public SpellData(float baseDamage, float size, float speed, float lifeTime)
    {
        this.baseDamage = baseDamage;
        this.size = size;
        this.speed = speed;
        this.lifeTime = lifeTime;
    }

    public SpellData(SpellData spellData)
    {
        this.baseDamage = spellData.baseDamage;
        this.size = spellData.size;
        this.speed = spellData.speed;
        this.lifeTime = spellData.lifeTime;
    }
}