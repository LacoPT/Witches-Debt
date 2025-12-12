using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Spell Config")]
public class SpellDataConfig : ScriptableObject
{
    public float DefaultDamage = 1;
    public float DefaultSpeed = 5f;
    public float DefaultScale = 1f;
}