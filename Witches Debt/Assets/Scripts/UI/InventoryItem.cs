using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Image image;
    private Transform parentAfterDrag;

    public void SetParent(Transform parAfterDrag)
    {
        parentAfterDrag = parAfterDrag;
    }
    // Drag and drop system
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Start Dragging");
        image.raycastTarget = false;
        var transform1 = transform;
        parentAfterDrag = transform1.parent;
        transform.SetParent(transform1.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging");
        transform.position = Mouse.current.position.ReadValue();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Dragging");
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
    }
}
