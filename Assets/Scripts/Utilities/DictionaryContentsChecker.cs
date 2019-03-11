using System;
using System.Collections.Generic;
using UnityEngine;

namespace Azarashi.Utilities
{
    /// <summary>
    /// Dictionaryのコンテンツと特定のクラス内のフィールド変数の対応関係の整合性チェックを提供します.
    /// ただし、利用可能なDictionaryのKeyはstring型のみに制限されます.
    /// </summary>
    /// <typeparam name="TSource">Dictinaryに対応するクラスの型</typeparam>
    /// <typeparam name="TValue">DictionaryのValueの型</typeparam>
    public class DictionaryContentsChecker<TSource,TValue>
    {
        public bool CheckConsistency(Dictionary<string, TValue> dictionary)
        {
            string[] keys = new string[dictionary.Keys.Count];
            dictionary.Keys.CopyTo(keys, 0);

            Type type = typeof(TSource);
            System.Reflection.FieldInfo[] infos = type.GetFields();

            if (keys.Length != infos.Length) return false;

            for (int i = 0; i < infos.Length; i++)
            {
                if (!infos[i].IsStatic && Array.IndexOf(keys, infos[i].Name) == -1)
                {
                    Debug.Log("The file's format is not match." +
                          Environment.NewLine + "'" + infos[i].Name + "' is not found.");
                    return false;
                }
            }

            return true;
        }
    }
}