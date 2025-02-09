using System.IO;
using UnityEngine;

namespace UltimateFolderPath
{
    /// <summary>
    /// A folder path relative to the streaming assets folder.
    /// </summary>
    [System.Serializable]
    public class StreamingAssetsPath : FolderPath
    {

        public StreamingAssetsPath(string path) : base(path)
        {
        }

        [SerializeField] public override string RelativeTo => Application.streamingAssetsPath;

        public static implicit operator StreamingAssetsPath(string path) => new StreamingAssetsPath(path);
    }
}