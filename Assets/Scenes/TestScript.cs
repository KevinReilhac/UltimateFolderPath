using System.Collections;
using System.Collections.Generic;
using System.IO;
using UltimateFolderPath.Editor;
using UnityEditor.Experimental;
using UnityEngine;

namespace UltimateFolderPath
{

    public class PrefabType : MonoBehaviour
    {
    }

    public class DesignData : ScriptableObject
    {
    }

    public class EditorDesignData : ScriptableObject
    {
    }


    public class TestScript : MonoBehaviour
    {

#region Runtime
        public ResourceFolderPath resourcesFolderPath = "Prefabs";
        public StreamingAssetsPath streamingAssetsFolderPath = "Textures";

        private void RuntimeMethod()
        {
            Debug.Log(resourcesFolderPath.AbsolutePath);
            // -> C:\Users\User\Documents\Unity\UnityProject\Assets\Resources\Prefabs
            Debug.Log(resourcesFolderPath.RelativePath);
            // -> Prefabs
            Debug.Log(resourcesFolderPath.RelativeTo);
            // -> C:\Users\User\Documents\Unity\UnityProject\Assets\Resources

            //Load a prefab (Prefab1) from the resources folder (in Assets/Resources/Prefabs/PrefabType)
            PrefabType prefabType = resourcesFolderPath.LoadAsset<PrefabType>("Prefabs/Prefab1");

            //Load a sprite from the streaming assets folder (Assets/StreamingAssets/Textures/Texture.png)
            Sprite sprite = streamingAssetsFolderPath.LoadSpriteFromFile("Texture.png");
        }
#endregion

#region Editor
        public AssetFolderPath assetFolderPath = "Data/DesignData";
        public ProjectFolderPath projectFolderPath = "ProjectSubFolder";
        public EditorResourcePath editorResourcePath = "EditorDesignData";

        private void EditorMethod()
        {
            //Load a design data (Level1) from the asset folder (in Assets/Data/DesignData/Level1)
            DesignData designData = assetFolderPath.LoadAsset<DesignData>("Level1");

            //Create a new folder in the project folder ([ProjectRoot]/ProjectSubFolder/NewFolder)
            projectFolderPath.CreateSubFolder("NewFolder");

            //Load an editor design data (EditorLevel1) from the editor resource folder (in Assets/EditorDefaultResources/EditorDesignData/EditorLevel1)
            EditorDesignData editorDesignData = editorResourcePath.LoadAsset<EditorDesignData>("EditorLevel1");
        }
#endregion
    }
}