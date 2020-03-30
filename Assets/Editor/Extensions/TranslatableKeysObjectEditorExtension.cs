using UnityEditor;
using UnityEngine;
using Azarashi.Utilities.Editor;

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
        }
    }
}