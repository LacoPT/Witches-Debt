using UnityEngine;
using UnityEngine.EventSystems;

public class SpellSlot : MonoBehaviour, IDropHandler
{
    public int index;
    public void SetIndex(int index) => this.index = index;
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            var inventoryItem = eventData.pointerDrag.GetComponent<InventoryItemUI>();
            inventoryItem.SetParent(transform);
        }
    }
}
