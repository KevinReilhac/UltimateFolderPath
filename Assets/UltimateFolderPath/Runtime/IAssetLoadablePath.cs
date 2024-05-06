using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UltimatePath
{
    public interface IAssetLoadableFolderPath
    {
        List<T> LoadAssets<T>() where T : Object;
        T LoadAsset<T>(string path) where T : Object;
    }
}