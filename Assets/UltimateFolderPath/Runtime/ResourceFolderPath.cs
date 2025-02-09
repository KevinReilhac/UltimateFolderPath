using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace UltimateFolderPath
{
    /// <summary>
    /// A folder path relative to the resource folder.
    /// Use Resources.Load to load an asset from the resource folder.
    /// </summary>
    ///
    [System.Serializable]
    public class ResourceFolderPath : FolderPath, IAssetLoadableFolderPath
    {
        #region Properties
        public override string RelativeTo => Path.Join(Application.dataPath, "Resources");
        #endregion

        #region Constructor
        public ResourceFolderPath(string path) : base(path) { }
        #endregion

        #region Asset Loading
        /// <summary>
        /// Load an asset from the resource folder.
        /// </summary>
        /// <typeparam name="T">The type of the asset.</typeparam>
        /// <param name="path">The path of the asset (extention not needed).</param>
        /// <returns>The asset.</returns>
        public T LoadAsset<T>(string path) where T : Object
        {
            return Resources.Load<T>(Path.Join(RelativePath, path));
        }

        /// <summary>
        /// /// Load all assets from the resource folder.
        /// </summary>
        /// <typeparam name="T">The type of the asset.</typeparam>
        /// <param name="recursive">If true, the assets will be loaded recursively.</param>
        /// <returns>A list of assets.</returns>
        public List<T> LoadAssets<T>(bool recursive = false) where T : Object
        {
            if (recursive)
            {
                Debug.LogWarning("Not implemented for now.");
                return new List<T>();
            }
            return Resources.LoadAll<T>(RelativePath).ToList();
        }
        #endregion

        #region Operators
        public static implicit operator ResourceFolderPath(string path) => new ResourceFolderPath(path);
        #endregion
    }
}