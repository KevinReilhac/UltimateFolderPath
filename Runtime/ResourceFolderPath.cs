using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace UltimateFolderPath
{
    public class ResourceFolderPath : FolderPath, IAssetLoadableFolderPath
    {
        public ResourceFolderPath(string path) : base(path)
        {
        }

        public override string RelativeTo => Path.Join(Application.dataPath, "Resources");

        public T LoadAsset<T>(string path) where T : Object
        {
            return Resources.Load<T>(Path.Join(RelativePath, path));
        }

        public List<T> LoadAssets<T>(bool recursive = false) where T : Object
        {
            if (recursive)
            {
                Debug.LogWarning("Not implemented for now.");
                return new List<T>();
            }
            return Resources.LoadAll<T>(RelativePath).ToList();
        }
    }
}