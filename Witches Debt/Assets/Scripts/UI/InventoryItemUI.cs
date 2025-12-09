using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryItemUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private InventoryItemConfig item;
    [SerializeField] private Image image;
    private InventoryController controller;
    private Transform parentAfterDrag;
    private InventoryController inventoryController;
    public InventoryItemConfig Item => item;
    public void SetInventoryModel(InventoryController inventoryController) => this.inventoryController = inventoryController;
    private void Awake()
    {
        InitializeItem(item);
    }
    public void InitializeItem(InventoryItemConfig newItem)
    {
        item = newItem;
        image.sprite = newItem.Image;
    }
    public void SetParent(Transform parAfterDrag)
    {
        parentAfterDrag = parAfterDrag;
    }
    // Drag and drop system
    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        var transform1 = transform;
        parentAfterDrag = transform1.parent;
        transform.SetParent(transform1.root);
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Mouse.current.position.ReadValue();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
    }
}
