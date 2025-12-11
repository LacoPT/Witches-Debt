public struct SpellData
{
    //THOSE ARE INTENDED TO BE CHANGED EXTERNALLY, BUT THIS IS BAD BECAUSE THIS IS HORRIBLE, FIX IT
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

    public SpellSaveData ToSaveData()
    {
        var data = new SpellSaveData();
        data.BaseDamage = baseDamage;
        data.Size = size;
        data.Speed = speed;
        return data;
    }

    public void FromSaveData(SpellSaveData data)
    {
        baseDamage = data.BaseDamage;
        size = data.Size;
        speed = data.Speed;
    }
}