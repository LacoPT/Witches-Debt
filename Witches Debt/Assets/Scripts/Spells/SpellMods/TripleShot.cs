using UnityEngine;

public class TripleShot : SpellMod
{
    private const float BASE_SIZE = 0.5f;
    private const float SPACING = 1f;
    private const float DISPERSION_RADIANS = Mathf.PI / 6;
        
    public override void Apply(Spell spell)
    {
        //TODO: Come up with a smarter way to do this
        if (spell.isClone) return;
        spell.OnSpawn.AddListener(() =>
        {
            var offsetAngle1 = DISPERSION_RADIANS;
            var offsetAngle2 = -DISPERSION_RADIANS;
            
            var offset1 = new Vector3(Mathf.Cos(offsetAngle1), Mathf.Sin(DISPERSION_RADIANS)) * SPACING;
            var offset2 = new Vector3(Mathf.Cos(offsetAngle2), Mathf.Sin(-DISPERSION_RADIANS)) * SPACING;
            
            var clone1 = GameObject.Instantiate(spell, spell.transform.position + offset1, spell.transform.rotation);
            var clone2 = GameObject.Instantiate(spell, spell.transform.position + offset2, spell.transform.rotation);
            clone1.isClone = true;
            clone2.isClone = true;

            spell.config.ApplyMods(clone1);
            spell.config.ApplyMods(clone2);
        });
    }
}