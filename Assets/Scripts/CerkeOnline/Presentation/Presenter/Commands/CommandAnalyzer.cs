using System;
using Azarashi.CerkeOnline.Domain.Entities;
using Azarashi.CerkeOnline.Domain.Entities.PublicDataType;

namespace Azarashi.CerkeOnline.Presentation.Presenter.Commands
{
    public class CommandAnalyzer
    {
        public IntegerVector2 GetPosition(string word)
        {
            if (word.IndexOf('-') >= 0)
            {
                string[] xy = word.Split('-');
                return new IntegerVector2(NumberStringToInt(xy[0]), NumberStringToInt(xy[1]));
            }

            int x = EnumNameToInt<Terminologies.BoardRow>((name) => word.StartsWith(name));
            if (x != -1) word = word.Remove(0, typeof(Terminologies.BoardRow).GetEnumName(x).Length);
            int y = EnumNameToInt<Terminologies.BoardLine>((name) => word == name);
            return new IntegerVector2(x, y);
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
            where T : struct, Enum
        {
            foreach (string name in Enum.GetNames(typeof(T)))
            {
                if (compare(name))
                    return (int)Enum.Parse(typeof(T), name, true);
            }

            return exception;
        }
    }
}