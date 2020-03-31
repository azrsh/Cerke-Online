using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Utf8Json;
using Azarashi.Utilities.Editor;
using Azarashi.CerkeOnline.Application.Language;

namespace Azarashi.CerkeOnline.EditorExtension
{
    [CustomEditor(typeof(TranslatableKeysObject))]
    public class TranslatableKeysObjectEditorExtension : Editor
    {
        static readonly string EnumName = "TranslatableKeys";
        static readonly string NameSpace = "Azarashi.CerkeOnline.Application.Language";

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EditorGUILayout.Space();


            if (GUILayout.Button("Generate Enum"))
            {
                TranslatableKeysObject translatableKeysObject = (TranslatableKeysObject)target;
                var text = EnumFileGenerator.Generate(EnumName, translatableKeysObject.TranslatableKeys,
                    "",
                    NameSpace);
                TextAssetExporter.Export(UnityEngine.Application.dataPath + "/Scripts/"
                    + NameSpace.Substring(9, NameSpace.Length - 9).Replace(".","/")
                    + "/" + EnumName + ".cs", text);
            }

            if(GUILayout.Button("Generate Empty Json"))
            {
                TranslatableKeysObject translatableKeysObject = (TranslatableKeysObject)target;
                var source = translatableKeysObject.TranslatableKeys.ToDictionary(key => key, key => string.Empty);
                var json = JsonSerializer.Serialize(source);
                GenerateDirectory(UnityEngine.Application.dataPath + "/Languages");
                GenerateDirectory(UnityEngine.Application.dataPath + "/Languages/empty");
                using (FileStream fileStream = new FileStream(UnityEngine.Application.dataPath + "/Languages/empty/words.json", FileMode.Create))
                {
                    fileStream.Write(json, 0, json.Length);
                }

                AssetDatabase.Refresh();
            }
        }

        void GenerateDirectory(string path)
        {
            if (!Directory.Exists(path)) 
                Directory.CreateDirectory(path);
        }
    }
}