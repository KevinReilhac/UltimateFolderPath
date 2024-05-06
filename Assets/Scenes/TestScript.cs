using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace UltimateFolderPath
{
    public class TestScript : MonoBehaviour
    {
        public AssetFolderPath assetFolderPath;

        private void Awake()
        {
            Debug.Log(assetFolderPath.AbsolutePath);
            Debug.Log(assetFolderPath.RelativePath);
        }
    }
}