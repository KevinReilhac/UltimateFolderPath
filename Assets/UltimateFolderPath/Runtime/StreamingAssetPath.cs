using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateFolderPath
{
    [System.Serializable]
    public class StreamingAssetsPath : FolderPath
    {
        public StreamingAssetsPath(string path) : base(path)
        {
        }

        [SerializeField] public override string RelativeTo => Application.streamingAssetsPath;
    }
}