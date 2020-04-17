using System;
using System.IO;
using System.Linq;
using Utf8Json;
using Azarashi.CerkeOnline.Domain.Entities;
using Azarashi.CerkeOnline.Domain.Entities.PublicDataType;
using Azarashi.CerkeOnline.Data.Repository;

namespace Azarashi.CerkeOnline.Data.DataStore
{
    public class LocalNoteDataStore : INoteDataStore
    {
        readonly string path;

        public LocalNoteDataStore(string name)
        {
            path = GetPath(name);
        }

        public NoteData Load()
        {
            string json = string.Empty;
            using (StreamReader streamReader = new StreamReader(path))
            {
                json = streamReader.ReadToEnd();
            }

            NoteJson jsonObject = JsonSerializer.Deserialize<NoteJson>(json);
            return JsonNoteParser.Parse(jsonObject);
        }

        public void Save(NoteData noteData)
        {
            throw new NotImplementedException();
        }

        private static string GetPath(string name)
        {
            return UnityEngine.Application.dataPath + "/Notes/" + name + ".json";
        }
    }
    internal class NoteJson
    {
        public readonly string blaxk;
        public readonly string red;
        public readonly string first;
        public readonly string season;
        public readonly string[] note;

        public NoteJson(string blaxk, string red, string first, string season, string[] note)
        {
            this.blaxk = blaxk;
            this.red = red;
            this.first = first;
            this.season = season;
            this.note = note;
        }
    }

    internal static class JsonNoteParser
    {
        public static NoteData Parse(NoteJson json)
        {
            var first = json.first == "red" ? Terminologies.PieceColor.Red : Terminologies.PieceColor.Black;
            var season = (Terminologies.Season)System.Enum.Parse(typeof(Terminologies.Season), System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(json.season));
            var movements = json.note.Select(ParseMovement).ToArray();

            return new NoteData(json.blaxk, json.red, first, season, movements);
        }

        static MovementData ParseMovement(string str)
        {
            var item = ReadOnlySubString.ToReadOnlySubString(str.ToUpper());
            IntegerVector2 start, via, end;
            Terminologies.PieceName pieceName;
            int waterEntryCast, steppingOverCast;
            SeasonContinueOrEnd continueOrEnd;
            item = ParsePosition(item, out start);
            item = CharToPieceName(item, out pieceName);
            item = ParsePosition(item, out via);
            item = ParsePosition(item, out end);
            item = ParseNumber(item, out waterEntryCast);
            item = ParseNumber(item, out steppingOverCast);
            ParseDeclaration(item, out continueOrEnd);

            return new MovementData(start, via, end, pieceName, waterEntryCast, steppingOverCast, continueOrEnd);
        }

        static ReadOnlySubString ParsePosition(ReadOnlySubString item, out IntegerVector2 position)
        {
            int x = EnumNameToInt<Terminologies.BoardRow>((name) => item.StartsWith(name));
            if (x != -1) item = item.Remove(typeof(Terminologies.BoardRow).GetEnumName(x).Length);

            int y = EnumNameToInt<Terminologies.BoardLine>((name) => item.StartsWith(name));
            if (y != -1) item = item.Remove(typeof(Terminologies.BoardRow).GetEnumName(y).Length);

            position = new IntegerVector2(x, y);
            return item;
        }

        static int EnumNameToInt<T>(Func<string, bool> compare, int exception = -1)
           where T : struct, Enum
        {
            foreach (string name in Enum.GetNames(typeof(T)))
            {
                if (compare(name))
                    return (int)Enum.Parse(typeof(T), name, true);
            }

            return exception;
        }

        static ReadOnlySubString CharToPieceName(ReadOnlySubString item, out Terminologies.PieceName pieceName)
        {
            char character = item.BeginOrDefault();
#if UNITY_EDITOR
            var pieceString = Enum.GetNames(typeof(Terminologies.PieceName)).Single(enumName => enumName[0] == character);
#else
            var pieceString = Enum.GetNames(typeof(Terminologies.PieceName)).SingleOrDefault(enumName => enumName[0] == character);
#endif
            Enum.TryParse(pieceString, out pieceName);

            return item.Remove(1);
        }

        static ReadOnlySubString ParseNumber(ReadOnlySubString item, out int number)
        {
            number = -1;
            if (!char.IsNumber(item.BeginOrDefault()))
                return item;

            number = int.Parse(item.BeginOrDefault().ToString());
            return item.Remove(1);
        }

        static ReadOnlySubString ParseDeclaration(ReadOnlySubString item, out SeasonContinueOrEnd continueOrEnd)
        {
            continueOrEnd = SeasonContinueOrEnd.Continue;
            if (item.BeginOrDefault() != '=')
                return item;

            if (item.StartsWith("TAXT"))
                continueOrEnd = SeasonContinueOrEnd.End;

            return ReadOnlySubString.Empty();
        }

        private struct ReadOnlySubString
        {
            public static ReadOnlySubString Empty() => ToReadOnlySubString(string.Empty);

            public static ReadOnlySubString ToReadOnlySubString(string str)
            {
                return new ReadOnlySubString(0, str);
            }

            readonly int index;
            readonly string body;

            private ReadOnlySubString(int index, string body)
            {
                this.index = index;
                this.body = body;
            }

            public bool StartsWith(string str)
            {
                if (body.Length - index < str.Length)
                    return false;

                for (int i = 0; i < str.Length; i++)
                    if (body[index + i] != str[i])
                        return false;

                return true;
            }

            public ReadOnlySubString Remove(int count)
            {
                if (index + count >= body.Length)
                    return Empty();
                return new ReadOnlySubString(index + count, body);
            }

            public char BeginOrDefault()
            {
                if (string.IsNullOrEmpty(body))
                    return default;

                return body[index];
            }
        }
    }
}