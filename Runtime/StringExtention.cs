using Unity.VisualScripting;
using UnityEngine;

namespace UltimateFolderPath
{
    public static class StringExtention
    {
        public static string ClearPath(this string str)
        {
            return str.Replace('\\', '/');
        }
    }
}