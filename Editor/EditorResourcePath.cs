using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace UltimateFolderPath.Editor
{
    /// <summary>
    /// A folder path relative to the editor default resources folder that can load assets.
    /// Use EditorGUIUtility.Load to load an asset from the editor default resources folder.
    /// </summary>
    [System.Serializable]
    public class EditorResourcePath : ProjectFolderPath, IAssetLoadableFolderPath
    {
        #region Properties
        public override string RelativeTo => Path.Join(Application.dataPath, "Editor Default Resources").ClearPath();
        #endregion

        #region Constructor
        public EditorResourcePath(string path) : base(path) { }
        #endregion

        #region Asset Loading
        /// <summary>
        /// Load an asset from the editor default resources folder.
        /// With EditorGUIUtility.Load, you can load an asset from the editor default resources folder.
        /// </summary>
        /// <typeparam name="T">The type of the asset.</typeparam>
        /// <param name="path">The path of the asset (extention is needed).</param>
        /// <returns>The asset.</returns>
        public T LoadAsset<T>(string path) where T : Object
        {
            string fullPath = Path.Join(RelativePath, path).ClearPath();
            T asset = EditorGUIUtility.Load(fullPath) as T;
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
        /// Load all assets from the editor default resources folder.
        /// </summary>
        /// <typeparam name="T">The type of the asset.</typeparam>
        /// <param name="recursive">If true, the assets will be loaded recursively.</param>
        /// <returns>The assets.</returns>
        public List<T> LoadAssets<T>(bool recursive = false) where T : Object
        {
            List<T> assetList = new List<T>();
            string[] files = GetAllFiles(false, recursive);

            foreach (var file in files)
            {
                T asset = LoadAsset<T>(file);
                if (asset != null) assetList.Add(asset);
            }

            return assetList;
        }
        #endregion

        #region Operators
        public static implicit operator EditorResourcePath(string path) => new EditorResourcePath(path);
        #endregion
    }
}