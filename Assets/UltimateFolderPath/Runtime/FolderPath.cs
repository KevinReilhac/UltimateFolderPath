using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using PathUtils = System.IO.Path;

namespace UltimateFolderPath
{
    [System.Serializable]
    public class FolderPath
    {
        [SerializeField] public string _path = null;

        public FolderPath(string path) { this._path = path; }

        public string AbsolutePath => PathUtils.Join(RelativeTo, _path).ClearPath();
        public string RelativePath => _path.ClearPath();
        public virtual string RelativeTo { get => null; }

        public string[] GetAllFiles(bool relative = true, bool recursive = false)
        {
            SearchOption searchOption = recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            string[] files = Directory.GetFiles(AbsolutePath, "*", searchOption);

            return relative ? files.Select(f => Path.GetRelativePath(RelativeTo, f)).ToArray() : files;
        }

    }
}