using UnityEngine;

[CreateAssetMenu(fileName = "SpellType", menuName = "Scriptable Objects/SpellType")]
public class SpellPrefabConfig : ScriptableObject
{
    //TODO: consider changing type
    [SerializeField] public Spell SpellPrefab;
}
