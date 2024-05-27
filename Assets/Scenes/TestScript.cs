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
            List<GameObject> gos = assetFolderPath.LoadAssets<GameObject>();

            foreach (GameObject go in gos)
            {
                Debug.Log(go.name);
            }
        }
    }
}