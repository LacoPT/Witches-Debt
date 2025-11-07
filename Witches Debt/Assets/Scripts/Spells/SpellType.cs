using UnityEngine;

//TODO: reconsider to use a simple enum, think where to have prefabs then or read some shit about zenject
//maybe this will help
[CreateAssetMenu(fileName = "SpellType", menuName = "Scriptable Objects/SpellType")]
public class SpellType : ScriptableObject
{
    //TODO: consider changing type
    [SerializeField] public Spell SpellPrefab;
}
