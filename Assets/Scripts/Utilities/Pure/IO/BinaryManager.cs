using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Azarashi.Utilities.IO
{
	public static class BinaryManager
	{
        // ファイル名を指定してデータをロード 
        public static T Load<T>(string path) where T : class
        {
            if (!File.Exists(path))
                return null;

            T data = null;
            using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                //読み込んで逆シリアル化する 
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                data = (T)binaryFormatter.Deserialize(fileStream);
            }
            return data;
        }

        // ファイルパスを指定してデータをセーブ 
        public static void Save<T>(string path, T data)
        {
            //シリアル化して書き込む 
            using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(fileStream, data);
            }
        }
	}
}
