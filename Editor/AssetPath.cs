using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;

namespace UltimateFolderPath
{
    public class AssetFolderPath : ProjectFolderPath, IAssetLoadableFolderPath
    {
        public AssetFolderPath(string path) : base(path)
        {
        }

        public override string RelativeTo => Application.dataPath;

        public T LoadAsset<T>(string path) where T : Object
        {
            return AssetDatabase.LoadAssetAtPath<T>(Path.Join(RelativePath, path));
        }

        public List<T> LoadAssets<T>() where T : Object
        {
            List<T> assetList = new List<T>();
            string[] files = GetAllFiles();

            foreach (var file in files)
            {
                T asset = AssetDatabase.LoadAssetAtPath<T>(file);
                if (asset != null) assetList.Add(asset);
            }

            return assetList;
        }
    }
}