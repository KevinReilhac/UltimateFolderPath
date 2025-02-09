using System.IO;
using System.Linq;
using UnityEngine;
using PathUtils = System.IO.Path;

namespace UltimateFolderPath
{
    /// <summary>
    /// The base class for all folder paths.
    /// </summary>
    [System.Serializable]
    public class FolderPath
    {

        public enum ImageFormat
        {
            PNG,
            JPG,
        }

        [SerializeField] private string _path = null;

        public FolderPath(string path) { this._path = path; }

        /// <summary>
        /// The absolute path of the folder.
        /// </summary>
        public string AbsolutePath => PathUtils.Join(RelativeTo, _path).ClearPath();

        /// <summary>
        /// The relative path of the folder.
        /// </summary>
        public string RelativePath => _path.ClearPath();

        /// <summary>
        /// What the path is relative to.
        /// /// </summary>
        public virtual string RelativeTo { get => null; }


        /// <summary>
        /// Get all files paths in the folder.
        /// </summary>
        /// <param name="relative">If true, the files will be relative to the folder.</param>
        /// <param name="recursive">If true, the files will be recursive.</param>
        /// <returns>An array of files paths (Relative).</returns>
        public string[] GetAllFiles(bool relative = true, bool recursive = false)
        {
            SearchOption searchOption = recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            string[] files = Directory.GetFiles(AbsolutePath, "*", searchOption);

            return relative ? files.Select(f => Path.GetRelativePath(RelativeTo, f)).ToArray() : files;
        }

        /// <summary>
        /// Get the text of a file.
        /// </summary>
        /// <param name="relativeFilePath">The path of the file (Relative).</param>
        /// <returns>The text of the file.</returns>
        public string GetTextFromFile(string relativeFilePath)
        {
            string filePath = Path.Combine(RelativeTo, relativeFilePath);
            if (!File.Exists(filePath))
            {
                Debug.LogError($"File at {filePath} does not exist");
                return null;
            }
            return File.ReadAllText(filePath);
        }

        /// <summary>
        /// Get the bytes of a file.
        /// </summary>
        /// <param name="relativeFilePath">The path of the file (Relative).</param>
        /// <returns>The bytes of the file.</returns>
        public byte[] GetBytesFromFile(string relativeFilePath)
        {
            string filePath = Path.Combine(RelativeTo, relativeFilePath);

            if (!File.Exists(filePath))
            {
                Debug.LogError($"File at {filePath} does not exist");
                return null;
            }

            byte[] bytes = File.ReadAllBytes(filePath);
            return bytes;
        }

        /// <summary>
        /// /// Load a texture2D from a file.
        /// </summary>
        /// <param name="relativeFilePath">The path of the file (Relative).</param>
        /// <returns>The texture2D of the file.</returns>
        public Texture2D LoadTexture2DFromFile(string relativeFilePath)
        {
            byte[] bytes = GetBytesFromFile(relativeFilePath);

            if (bytes == null)
                return null;

            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(bytes);
            return texture;
        }

        /// <summary>
        /// Load a sprite from a file.
        /// </summary>
        /// <param name="relativeFilePath">The path of the file (Relative).</param>
        /// <returns>The sprite of the file.</returns>
        public Sprite LoadSpriteFromFile(string relativeFilePath)
        {
            Texture2D texture = LoadTexture2DFromFile(relativeFilePath);
            if (texture == null)

                return null;

            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }

        /// <summary>
        /// Check if a sub folder exists.
        /// </summary>
        /// <param name="subFolderPath">The path of the sub folder (Relative).</param>
        /// <returns>True if the sub folder exists, false otherwise.</returns>
        public bool IsSubFolderExist(string subFolderPath)
        {
            string subFolderFullPath = Path.Combine(RelativeTo, subFolderPath);
            return Directory.Exists(subFolderFullPath);
        }


        /// <summary>
        /// Create a sub folder.
        /// </summary>
        /// <param name="subFolderPath">The path of the sub folder (Relative).</param>
        public void CreateSubFolder(string subFolderPath)
        {
            string subFolderFullPath = Path.Combine(RelativeTo, subFolderPath);
            if (Directory.Exists(subFolderFullPath))
                return;
            Directory.CreateDirectory(subFolderFullPath);
        }

        /// <summary>
        /// Write a file.
        /// </summary>
        /// <param name="relativeFilePath">The path of the file (Relative).</param>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="content">The content of the file.</param>
        public void WriteFile(string relativeFilePath, string fileName, string content)
        {
            string folderPath = Path.Combine(RelativeTo, relativeFilePath);
            if (!Directory.Exists(folderPath))

                Directory.CreateDirectory(folderPath);

            string filePath = Path.Combine(folderPath, fileName);

            File.WriteAllText(filePath, content);
        }

        /// <summary>
        /// Write a file.
        /// </summary>
        /// <param name="relativeFilePath">The path of the file (Relative).</param>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="content">The content of the file.</param>
        public void WriteFile(string relativeFilePath, string fileName, byte[] content)
        {
            string folderPath = Path.Combine(RelativeTo, relativeFilePath);
    
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            string filePath = Path.Combine(folderPath, fileName);

            File.WriteAllBytes(filePath, content);
        }

        /// <summary>
        /// /// Write an image to a file.
        /// </summary>
        /// <param name="relativeFilePath">The path of the file (Relative).</param>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="texture">The texture to write.</param>
        /// <param name="imageFormat">The format of the image.</param>
        public void WriteImage(string relativeFilePath, string fileName, Texture2D texture, ImageFormat imageFormat = ImageFormat.PNG)
        {
            byte[] bytes = imageFormat == ImageFormat.PNG ? texture.EncodeToPNG() : texture.EncodeToJPG();

            WriteFile(relativeFilePath, fileName, bytes);
        }

        /// <summary>
        /// /// Delete a file.
        /// </summary>
        /// <param name="relativeFilePath">The path of the file (Relative).</param>
        /// <param name="fileName">The name of the file.</param>
        public void DeleteFile(string relativeFilePath, string fileName)
        {
            string filePath = Path.Combine(RelativeTo, relativeFilePath, fileName);
            if (File.Exists(filePath))
                File.Delete(filePath);
            else
                Debug.LogError($"File at {filePath} does not exist");
        }

        /// <summary>
        /// Delete a folder.
        /// </summary>
        /// <param name="relativeFolderPath">The path of the folder (Relative).</param>
        public void DeleteFolder(string relativeFolderPath)
        {
            string folderPath = Path.Combine(RelativeTo, relativeFolderPath);


            if (Directory.Exists(folderPath))
                Directory.Delete(folderPath, true);
            else
                Debug.LogError($"Folder at {folderPath} does not exist");
        }

        public static implicit operator FolderPath(string path) => new FolderPath(path);
    }
}