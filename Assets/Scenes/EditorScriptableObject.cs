using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UltimateFolderPath.Editor;

[CreateAssetMenu(fileName = "EditorScriptableObject", menuName = "UltimateFolderPath/EditorScriptableObject")]
public class EditorScriptableObject : ScriptableObject
{
    [Space]
    public EditorResourcePath editorResourcePath = "EditorDesignData";
    [Space]
    public AssetFolderPath assetFolderPath = "Data/DesignData";
    [Space]
    public ProjectFolderPath projectFolderPath = "ProjectSubFolder";
}
