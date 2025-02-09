using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace UltimateFolderPath.Editor
{
    /// <summary>
    /// A folder path relative to the asset folder that can load assets.
    /// Use AssetDatabase.LoadAssetAtPath to load an asset from the asset folder.
    /// </summary>
    [System.Serializable]
    public class AssetFolderPath : ProjectFolderPath, IAssetLoadableFolderPath
    {
        #region Properties
        public override string RelativeTo => Application.dataPath;
        public string AssetPath => Path.Join("Assets", RelativePath).ClearPath();
        #endregion

        #region Constructor
        public AssetFolderPath(string path) : base(path) { }
        #endregion

        #region Asset Loading
        /// <summary>
        /// Load an asset from the asset folder.
        /// Use AssetDatabase.LoadAssetAtPath to load the asset.
        /// </summary>
        /// <typeparam name="T">The type of the asset.</typeparam>
        /// <param name="path">The path of the asset (extention is needed).</param>
        /// <returns>The asset.</returns>
        public T LoadAsset<T>(string path) where T : Object
        {
            string fullPath = GetAssetFilePath(path);

            T asset = AssetDatabase.LoadAssetAtPath<T>(fullPath);
            if (asset == null)
            {
                if (path.Contains("."))
                    Debug.LogError($"Asset not found: {fullPath}\nIt looks like you forgot to add the extension to the path.");
                else
                    Debug.LogError($"Asset not found: {fullPath}");
            }

            return asset;
        }

        /// <summary>
        /// Load all assets from the asset folder.
        /// </summary>
        /// <typeparam name="T">The type of the asset.</typeparam>
        /// <param name="recursive">If true, the assets will be loaded recursively.</param>
        /// <returns>The assets.</returns>
        public List<T> LoadAssets<T>(bool recursive = false) where T : Object
        {
            List<T> assetList = new List<T>();
            string[] files = GetAllFiles(false, recursive);

            string cleanFileName = null;
            foreach (var file in files)
            {
                cleanFileName = file.Replace(AbsolutePath, "");
                T asset = AssetDatabase.LoadAssetAtPath<T>(GetAssetFilePath(cleanFileName));
                if (asset != null) assetList.Add(asset);
            }

            return assetList;
        }
        #endregion

        #region Path Utilities
        /// <summary>
        /// Get the full path of the asset file.
        /// </summary>
        /// <param name="fileName">The name of the asset file.</param>
        /// <returns>The full path of the asset file.</returns>
        public string GetAssetFilePath(string fileName)
        {
            return Path.Join(AssetPath, fileName).ClearPath();
        }
        #endregion

        #region Operators
        public static implicit operator AssetFolderPath(string path) => new AssetFolderPath(path);
        #endregion
    }
}