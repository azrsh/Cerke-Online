using System.IO;
using System.Collections.Generic;
using System.Linq;
using Azarashi.CerkeOnline.Data.Repository;

namespace Azarashi.CerkeOnline.Data.DataStore
{
    public class LocalNoteListDataStore : INoteListDataStore
    {
        readonly string path;

        public LocalNoteListDataStore()
        {
            path = GetDirectoryPath();
        }

        public IEnumerable<string> Load()
        {
            return Directory.GetDirectories(path).Select(Path.GetFileNameWithoutExtension);
        }

        private static string GetDirectoryPath()
        {
            return UnityEngine.Application.dataPath + "/Notes";
        }
    }
}