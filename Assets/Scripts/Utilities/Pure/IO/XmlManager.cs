using System;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;


namespace Azarashi.Utilities.IO
{
	public class XmlManager
	{
		public static void Save<T>(string path,T obj)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(T));
            // カレントディレクトリに"settings.xml"というファイルで書き出す
            using (FileStream fs = new FileStream(Directory.GetCurrentDirectory() + "\\" + path, FileMode.Create))
            {
                // オブジェクトをシリアル化してXMLファイルに書き込む
                serializer.Serialize(fs, obj);
            }
		}
		
		public static T Load<T>(string path)
		{
            if (!File.Exists(Directory.GetCurrentDirectory() + "\\" + path))
                return default(T);

            T data;
			XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (FileStream fs = new FileStream(Directory.GetCurrentDirectory() + "\\" + path, FileMode.Open))
            {
                // XMLファイルを読み込み、逆シリアル化（復元）する
                data = (T)serializer.Deserialize(fs);
            }
            return data;
		}
		
		
		//--------シリアライズできないDictionaryを変換するメソッド-----------------------
		
		/// <summary>
		/// シリアル化できる、KeyValuePairに代わる構造体
		/// </summary>
		/// <typeparam name="TKey">Keyの型</typeparam>
		/// <typeparam name="TValue">Valueの型</typeparam>
		[Serializable]
		public struct KeyAndValue<TKey, TValue>
		{
			public TKey Key;
			public TValue Value;
			
			public KeyAndValue(KeyValuePair<TKey, TValue> pair)
			{
				Key = pair.Key;
				Value = pair.Value;
			}
		}
		
		/// <summary>
		/// DictionaryをKeyAndValueのListに変換する
		/// </summary>
		/// <typeparam name="TKey">Dictionaryのキーの型</typeparam>
		/// <typeparam name="TValue">Dictionaryの値の型</typeparam>
		/// <param name="dic">変換するDictionary</param>
		/// <returns>変換されたKeyAndValueのList</returns>
		public static List<KeyAndValue<TKey, TValue>>
			ConvertDictionaryToList<TKey, TValue>(Dictionary<TKey, TValue> dic)
		{
			List<KeyAndValue<TKey, TValue>> lst =
				new List<KeyAndValue<TKey, TValue>>();
			foreach (KeyValuePair<TKey, TValue> pair in dic)
			{
				lst.Add(new KeyAndValue<TKey, TValue>(pair));
			}
			return lst;
		}
		
		/// <summary>
		/// KeyAndValueのListをDictionaryに変換する
		/// </summary>
		/// <typeparam name="TKey">KeyAndValueのKeyの型</typeparam>
		/// <typeparam name="TValue">KeyAndValueのValueの型</typeparam>
		/// <param name="lst">変換するKeyAndValueのList</param>
		/// <returns>変換されたDictionary</returns>
		public static Dictionary<TKey, TValue>
			ConvertListToDictionary<TKey, TValue>(List<KeyAndValue<TKey, TValue>> lst)
		{
			Dictionary<TKey, TValue> dic = new Dictionary<TKey, TValue>();
			foreach (KeyAndValue<TKey, TValue> pair in lst)
			{
				dic.Add(pair.Key, pair.Value);
			}
			return dic;
		}
		
	}
}
