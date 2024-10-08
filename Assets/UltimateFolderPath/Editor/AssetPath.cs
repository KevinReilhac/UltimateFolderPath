using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace UltimateFolderPath
{
    [System.Serializable]
    public class AssetFolderPath : ProjectFolderPath, IAssetLoadableFolderPath
    {
        public AssetFolderPath(string path) : base(path)
        {
        }

        public override string RelativeTo => Application.dataPath;

        public T LoadAsset<T>(string path) where T : Object
        {
            return AssetDatabase.LoadAssetAtPath<T>(GetAssetFilePath(path));
        }

        public string AssetPath => Path.Join("Assets", RelativePath).ClearPath();

        public string GetAssetFilePath(string fileName)
        {
            return Path.Join(AssetPath, fileName).ClearPath();
        }

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
    }
}