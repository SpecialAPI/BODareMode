using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadFromFileExample : MonoBehaviour
{
    [Header("File Names")]
    public string assetBundleNameToLoad;
    public string prefabNameToLoad;

    void Start()
    {
        var myLoadedAssetBundle
            = AssetBundle.LoadFromFile(Path.Combine(Application.dataPath, "AssetBundles", assetBundleNameToLoad));
        if (myLoadedAssetBundle == null)
        {
            Debug.Log("Failed to load AssetBundle!");
            return;
        }
        var prefab = myLoadedAssetBundle.LoadAsset<GameObject>(prefabNameToLoad);
        Instantiate(prefab);
    }
}
