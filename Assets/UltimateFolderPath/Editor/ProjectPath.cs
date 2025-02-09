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
        #region Properties
        public override string RelativeTo => GetProjectFolder();
        #endregion

        #region Constructor
        public ProjectFolderPath(string path) : base(path) { }
        #endregion

        #region Path Utilities
        private string GetProjectFolder()
        {
            return string.Join("/", Application.dataPath.Split('\\', '/').SkipLast(1));
        }
        #endregion

        #region Operators
        public static implicit operator ProjectFolderPath(string path) => new ProjectFolderPath(path);
        #endregion
    }
}