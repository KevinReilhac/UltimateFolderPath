using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UltimateFolderPath.Editor
{
    /// <summary>
    /// A folder path relative to the root project folder.
    /// </summary>
    [System.Serializable]
    public class ProjectFolderPath : FolderPath

    {

        public ProjectFolderPath(string path) : base(path)
        {
        }

        public override string RelativeTo => GetProjectFolder();

        private string GetProjectFolder()
        {
            return string.Join("/", Application.dataPath.Split('\\', '/').SkipLast(1));
        }

        public static implicit operator ProjectFolderPath(string path) => new ProjectFolderPath(path);
    }
}