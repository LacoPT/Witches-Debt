using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Scriptable Objects/InventoryItem")]
public class InventoryItemSO : ScriptableObject
{
    [SerializeField] private Sprite image;
    [SerializeField] private Rarity rarity;
    [SerializeField] private ItemType type;
    [FormerlySerializedAs("name")]
    [Header("Text Fields")]
    [SerializeField] private LocalizedString itemName;
    [SerializeField] private LocalizedString description;
    public ItemType ItemType => type;
    public Rarity Rarity => rarity;
    public Sprite Image => image;
    public string Name => itemName.GetLocalizedString();
    public string Description => description.GetLocalizedString();
}