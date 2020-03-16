using System.IO;

namespace Azarashi.Utilities.IO
{
    public static class TextManager
    {
        // テキストファイルをロード 
        public static string Load(string path)
        {
            if (!File.Exists(path))
                return null;

            string textData = null;
            FileInfo fileInfo = new FileInfo(path);
            using (StreamReader sr = new StreamReader(fileInfo.OpenRead()))
            {
                textData = sr.ReadToEnd();
            }
            fileInfo = null;
            return textData;
        }

        // テキストファイルをセーブ 
        public static void Save(string path, string textData)
        {
            FileInfo fi = new FileInfo(path);
            using (StreamWriter streamWriter = fi.AppendText())
            {
                streamWriter.Write(textData);
                streamWriter.Flush();
            }
        }
    }
}