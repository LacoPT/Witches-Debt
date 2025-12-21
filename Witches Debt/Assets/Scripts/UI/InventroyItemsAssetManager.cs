using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class InventoryItemsAssetManager : MonoBehaviour
{
    public static InventoryItemsAssetManager Instance;
    private Dictionary<string, InventoryItemConfig> AssetsCash = new Dictionary<string, InventoryItemConfig>();
    
    public void Awake()
    {
        Instance = this;
    }

    public InventoryItemConfig GetItemConfig(string address)
    {
        if (AssetsCash.TryGetValue(address, out var config)) return config;
        
        var loadedConfig = Resources.Load<InventoryItemConfig>(address);
        AssetsCash.Add(address, loadedConfig);
        return loadedConfig;
    }
}
