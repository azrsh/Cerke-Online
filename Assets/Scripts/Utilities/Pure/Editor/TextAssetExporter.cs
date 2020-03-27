using System.Text;
using System.IO;
using UnityEditor;

namespace Azarashi.Utilities.Editor
{
    public static class TextAssetExporter
    {
        public static void Export(string path, string data)
        {
            File.WriteAllText(path, data, Encoding.UTF8);
            AssetDatabase.Refresh(ImportAssetOptions.ImportRecursive);
        }
    }
}