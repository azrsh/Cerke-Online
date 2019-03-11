using System;
using UnityEngine;
using Azarashi.CerkeOnline.Domain.Entities;

namespace Azarashi.CerkeOnline.Presentation.Presenter.Commands
{
    public class CommandAnalyzer
    {
        public Vector2Int GetPosition(string word)
        {
            if (word.IndexOf('-') >= 0)
                return new Vector2Int(NumberStringToInt(word.Split('-')[0]), NumberStringToInt(word.Split('-')[1]));

            Vector2Int result = new Vector2Int(-1, -1);
            result.x = EnumNameToInt<Terminologies.BoardRow>((name) => word.StartsWith(name));
            if (result.x != -1) word = word.Remove(0, typeof(Terminologies.BoardRow).GetEnumName(result.x).Length);
            result.y = EnumNameToInt<Terminologies.BoardLine>((name) => word == name);
            return result;
        }

        public bool CheckFormat(string[] words, int wordCount)
        {
            return words.Length == wordCount;
        }

        int NumberStringToInt(string value, int exception = -1)
        {
            int result = exception;
            int.TryParse(value, out result);
            return result;
        }

        int EnumNameToInt<T>(Func<string, bool> compare, int exception = -1)
            where T : struct
        {
            foreach (string name in Enum.GetNames(typeof(T)))
            {
                if (!compare(name)) continue;
                return (int)Enum.Parse(typeof(T), name);
            }

            return exception;
        }
    }
}