using UnityEngine;
using UnityEngine.EventSystems;

public class SpellSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            var inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            inventoryItem.SetParent(transform);
        }
    }
}
