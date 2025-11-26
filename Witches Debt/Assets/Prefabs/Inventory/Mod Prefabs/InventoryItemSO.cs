using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(menuName = "Scriptable Objects/InventoryItem")]
public class InventoryItemSO : ScriptableObject
{
    [SerializeField] private Sprite image;
    [SerializeField] private Rarity rarity;
    [SerializeField] private ItemType type;
    
    [Header("Text Fields")]
    [SerializeField] private LocalizedString name;
    [SerializeField] private LocalizedString description;
    
    public ItemType ItemType => type;
    public Rarity Rarity => rarity;
    public Sprite Image => image;
    public string Name => name.GetLocalizedString();
    public string Description => description.GetLocalizedString();
}