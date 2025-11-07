using UnityEngine;

[CreateAssetMenu(fileName = "SpellType", menuName = "Scriptable Objects/SpellType")]
public class SpellType : ScriptableObject
{
    //TODO: consider changing type
    [SerializeField] public GameObject SpellPrefab;
}
