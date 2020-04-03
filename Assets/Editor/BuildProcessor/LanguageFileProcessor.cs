using System.IO;
using UnityEngine;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

public class LanguageFileProcessor : IPostprocessBuildWithReport
{
    public void OnPostprocessBuild(BuildReport report)
    {
        var directoryPath = Application.dataPath + "/Languages";
        var outputPath = report.summary.outputPath;
        var nextPath = Path.GetDirectoryName(outputPath) + "/" + Path.GetFileNameWithoutExtension(outputPath) + "_data/Languages";
        
        Debug.Log("Copy" + directoryPath + "To" + nextPath);

        CopyDirectory(directoryPath, nextPath);

        var metaFiles = Directory.GetFiles(nextPath, "*.meta", SearchOption.AllDirectories);
        foreach (var item in metaFiles)
            File.Delete(item);
    }

    // 実行順
    public int callbackOrder { get { return 0; } }


    static void CopyDirectory(string sourceDirectoryName, string targetDirectoryName)
    {
        DirectoryInfo source = new DirectoryInfo(sourceDirectoryName);

        if (!source.Exists)
            return;

        if (!Directory.Exists(targetDirectoryName))
            Directory.CreateDirectory(targetDirectoryName);

        FileInfo[] files = source.GetFiles();
        foreach (FileInfo file in files)
        {
            string temppath = Path.Combine(targetDirectoryName, file.Name);
            file.CopyTo(temppath, false);
        }

        DirectoryInfo[] subDirectories = source.GetDirectories();
        foreach (DirectoryInfo subDirectory in subDirectories)
        {
            string temppath = Path.Combine(targetDirectoryName, subDirectory.Name);
            CopyDirectory(subDirectory.FullName, temppath);
        }
    }
}
