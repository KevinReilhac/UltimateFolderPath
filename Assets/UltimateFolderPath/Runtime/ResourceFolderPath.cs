using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace UltimatePath
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

        public List<T> LoadAssets<T>() where T : Object
        {
            return Resources.LoadAll<T>(RelativePath).ToList();
        }
    }
}