using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using PathUtils = System.IO.Path;

namespace UltimatePath
{
    [System.Serializable]
    public class FolderPath
    {
        [SerializeField] public string _path = null;

        public FolderPath(string path) {this._path = path;}

        public string AbsolutePath => PathUtils.Join(RelativeTo, _path).ClearPath();
        public string RelativePath => _path.ClearPath();
        public virtual string RelativeTo {get => null;}

        public string[] GetAllFiles(bool relative = true)
        {
            string[] files = Directory.GetFiles(AbsolutePath);
            return files.Select(f => Path.GetRelativePath(RelativeTo, f)).ToArray();
        }

        public static implicit operator string(FolderPath f) => f.RelativePath;
    }
}