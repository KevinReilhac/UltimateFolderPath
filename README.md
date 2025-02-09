# UltimateFolderPath
Using path string fields can quickly become inconvenient, some methods require relative paths, while others need absolute ones. This package aims to solve that problem.

## Authors

- [@Kévin "Kebab" Reilhac](https://www.github.com/KevinReilhac)


## Installation

Install UltimateFolderPath with Unity Package Manager

```bash
  https://github.com/KevinReilhac/UltimateFolderPath.git#upm
```

## Easy path setup
You can set your path with the file explorer instead of a simple string fields
![exempleassetfolderpath](https://github.com/user-attachments/assets/26eb34f2-0e58-4ce3-8002-0625c7320b5c)

A red field means that the folder is not exist

![image](https://github.com/user-attachments/assets/89f5c82f-eef4-4fe0-b574-98ef0037b9f7)

## Many FolderPath types
### Runtime FolderPath
- `ResourcesFolderPath` : Relative to Resources folder. Load assets with ```Resources.Load```
- `StreamingAssetsFolderPath` : Relative to StreamingAssets folder.

```csharp
public ResourceFolderPath resourcesFolderPath = "Prefabs";
public StreamingAssetsPath streamingAssetsFolderPath = "Textures";

private void RuntimeMethod()
{
    //You can access the absolute path, relative path, and relative to path of the folder path.

    Debug.Log(resourcesFolderPath.AbsolutePath);
    // -> C:\Users\User\Documents\Unity\UnityProject\Assets\Resources\Prefabs
    Debug.Log(resourcesFolderPath.RelativePath);
    // -> Prefabs
    Debug.Log(resourcesFolderPath.RelativeTo);
    // -> C:\Users\User\Documents\Unity\UnityProject\Assets\Resources

    //You can load assets from the folder path.
    //Load a prefab (Prefab1) from the resources folder (in Assets/Resources/Prefabs/PrefabType)
    PrefabType prefabType = resourcesFolderPath.LoadAsset<PrefabType>("Prefabs/Prefab1");

    //Load a sprite from the streaming assets folder (Assets/StreamingAssets/Textures/Texture.png)
    Sprite sprite = streamingAssetsFolderPath.LoadSpriteFromFile("Texture.png");
}
```

### Editor FolderPath
- `AssetFolderPath` : Relative to Assets folder Load assets with ```AssetDatabase.LoadAssetAtPath```
- `ProjectFolderPath` : Relative to root folder path.
- `Editor Resources path `Relative to Editor Default Resources folder. Load assets with ```EditorGUIUtility.Load```

```csharp
public AssetFolderPath assetFolderPath = "Data/DesignData";
public ProjectFolderPath projectFolderPath = "ProjectSubFolder";
public EditorResourcePath editorResourcePath = "EditorDesignData";

private void EditorMethod()
{
    //Load all design data from the asset folder (in Assets/Data/DesignData)
    List<DesignData> designDataList = assetFolderPath.LoadAssets<DesignData>();

    //Create a new folder in the project folder ([ProjectRoot]/ProjectSubFolder/NewFolder)
    projectFolderPath.CreateSubFolder("NewFolder");

    //Load an editor design data (EditorLevel1) from the editor resource folder (in Assets/EditorDefaultResources/EditorDesignData/EditorLevel1)
    EditorDesignData editorDesignData = editorResourcePath.LoadAsset<EditorDesignData>("EditorLevel1");
}
```

## Documentation

[Read Documentation](https://kevinreilhac.github.io/UltimateFolderPath/)
