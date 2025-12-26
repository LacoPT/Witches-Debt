using System;
using UnityEngine;
using UnityEngine.InputSystem;

[Obsolete]
public class TestPlayerInitalizer : MonoBehaviour
{
    public void TestInitializeFromInventory(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        var loader = GetComponent<SpellLoader>();
        loader.ClearAllCasters();
        //loader.LoadFromInventoryModel(InventoryModel.GetInstance());
    }
}