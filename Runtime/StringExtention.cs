using Unity.VisualScripting;
using UnityEngine;

namespace UltimatePath
{
    internal static class StringExtention
    {
        public static string ClearPath(this string str)
        {
            return str.Replace('\\', '/');
        }
    }
}