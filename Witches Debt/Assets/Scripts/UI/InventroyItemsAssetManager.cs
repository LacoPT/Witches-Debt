using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class InventroyItemsAssetManager : MonoBehaviour
{
    private static InventroyItemsAssetManager Instance;
    private Dictionary<string, InventoryItemConfig> AssetsCash = new Dictionary<string, InventoryItemConfig>();
    private Dictionary<string, AsyncOperationHandle<InventoryItemConfig>> ActiveHandles = new Dictionary<string, AsyncOperationHandle<InventoryItemConfig>>();

    private void Awake()
    {
        Instance = this;
    }

    public async Task<InventoryItemConfig> GetItemConfig(string address)
    {
        if (AssetsCash.TryGetValue(address, out InventoryItemConfig inventoryItemConfig));
        
        var handle = Addressables.LoadAssetAsync<InventoryItemConfig>(address);
        await handle.Task;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            var result = handle.Result;
            
            AssetsCash[address] = result;
            ActiveHandles[address] = handle;
            
            return result;
        }
        
        return null;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created

}
