using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace UltimateFolderPath
{
    [CustomPropertyDrawer(typeof(FolderPath), true)]
    internal class PathPropertyDrawer : PropertyDrawer
    {
        #region Constants
        private const string BUTTON_ICON_NAME = "d_Folder Icon";
        private const string PATH_PROPERTY = "_path";
        private const float BUTTON_WITH = 30f;
        #endregion

        #region Fields
        private static readonly Color errorColor = Color.red;
        private string lastCheckedPath = null;
        private bool checkedValue = true;
        #endregion

        #region GUI Drawing
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty pathProperty = property.FindPropertyRelative(PATH_PROPERTY);
            EditorGUI.BeginProperty(position, label, property);
            // Draw label
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            // Don't make child fields be indented
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            // Calculate rects
            Rect fieldRect = new Rect(position.x, position.y, position.width - BUTTON_WITH, position.height);
            Rect buttonRect = new Rect(position.x + (position.width - BUTTON_WITH), position.y, BUTTON_WITH, position.height);
            if (lastCheckedPath != null || lastCheckedPath != pathProperty.stringValue)
            {
                checkedValue = CheckPath(property);
                lastCheckedPath = pathProperty.stringValue;
            }
            DrawPathField(fieldRect, pathProperty);
            if (GUI.Button(buttonRect, EditorGUIUtility.IconContent(BUTTON_ICON_NAME)))
                OpenFolder(property, label.text);

            // Set indent back to what it was
            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }

        private void DrawPathField(Rect rect, SerializedProperty property)
        {
            Color defaultColor = GUI.backgroundColor;
            GUI.enabled = false;
            GUI.backgroundColor = checkedValue ? defaultColor : errorColor;
            EditorGUI.TextField(rect, property.stringValue);
            GUI.backgroundColor = defaultColor;
            GUI.enabled = true;
        }
        #endregion

        #region Folder Operations
        private void OpenFolder(SerializedProperty property, string label)
        {
            string relativeTo = RelativeTo(property);
            string result = null;

            if (relativeTo != null)
            {
                result = EditorUtility.OpenFolderPanel(label, relativeTo, "");
                if (!result.StartsWith(relativeTo))
                {
                    Debug.LogErrorFormat("Path must be inside {0} folder.", relativeTo);
                    return;
                }
                result = Path.GetRelativePath(relativeTo, result);
            }
            else
            {
                result = EditorUtility.OpenFolderPanel(label, Application.dataPath, "");
            }

            if (string.IsNullOrEmpty(result)) return;
            property.FindPropertyRelative(PATH_PROPERTY).stringValue = result;
            property.serializedObject.ApplyModifiedProperties();
        }

        private string RelativeTo(SerializedProperty property)
        {
            FolderPath instance = GetInstance(property);

            return instance.RelativeTo;
        }

        private bool CheckPath(SerializedProperty serializedProperty)
        {
            FolderPath folderPath = GetInstance(serializedProperty);

            if (folderPath == null)
                return false;
            return Directory.Exists(folderPath.AbsolutePath);
        }
        #endregion

        #region Utilities
        private FolderPath GetInstance(SerializedProperty property)
        {
            var targetObject = property.serializedObject.targetObject;
            var targetObjectClassType = targetObject.GetType();
            var field = targetObjectClassType.GetField(property.propertyPath, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (field != null)
            {
                var value = field.GetValue(targetObject);
                return value as FolderPath;
            }

            return null;
        }
        #endregion
    }
}