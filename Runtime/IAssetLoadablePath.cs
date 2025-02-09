using System.Collections.Generic;
using UnityEngine;

namespace UltimateFolderPath
{
    /// <summary>
    /// Interface for FolderPath that can load assets.
    /// </summary>
    public interface IAssetLoadableFolderPath
    {
        List<T> LoadAssets<T>(bool recursive = false) where T : Object;
        T LoadAsset<T>(string path) where T : Object;
    }
}