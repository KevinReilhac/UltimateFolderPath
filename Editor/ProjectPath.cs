using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UltimatePath
{
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
    }
}