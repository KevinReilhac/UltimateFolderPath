using Unity.VisualScripting;
using UnityEngine;

namespace UltimateFolderPath
{
    internal static class StringExtention
    {
        public static string ClearPath(this string str)
        {
            return str.Replace('\\', '/');
        }
    }
}